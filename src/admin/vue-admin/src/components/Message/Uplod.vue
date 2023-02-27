<template>
  <v-card>
    <v-toolbar flat dense color="transparent">
      <v-toolbar-title>{{ title }}</v-toolbar-title>
      <v-spacer></v-spacer>
    </v-toolbar>

    <v-card-text>
      <div v-for="(arr_files, arr_files_index) in files" :key="arr_files_index">
        <v-file-input
          chips
          counter
          multiple
          show-size
          small-chips
          truncate-length="15"
          dene
          v-model="arr_files.files"
          :readonly="uploading"
        ></v-file-input>
        <v-layout
          v-for="(state, stateIndex) in arr_files.status"
          :key="stateIndex"
          wrap
        >
          <v-flex xs12>
            <v-subheader>{{ state.file_name }}:</v-subheader>
          </v-flex>
          <v-flex xs10>
            <v-progress-linear :value="state.progress" height="25" bottom>
              <strong>{{ state.progress.toFixed(2) }}%</strong>
            </v-progress-linear>
          </v-flex>
          <v-flex xs2>
            <v-btn icon small title="开始">
              <v-icon color="primary">play_arrow</v-icon>
            </v-btn>
            <v-btn icon small title="暂停">
              <v-icon color="primary">pause</v-icon>
            </v-btn>
            <v-btn icon small title="停止">
              <v-icon color="primary">stop</v-icon>
            </v-btn>
          </v-flex>
        </v-layout>
      </div>
    </v-card-text>
    <v-card-actions>
      <v-btn text @click="cancel" color="primary">取消</v-btn>
      <v-spacer></v-spacer>
      <v-btn
        color="primary"
        text
        @click="success"
        :disabled="values.length == 0"
        >确认
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import { upload_bid_file } from "./file_splice";
/**
 * 定义上传状态
 */
const upload_state = {
  /**
   * 上传中
   */
  upload_ing: Symbol(),

  /**
   * 暂停中
   */
  pause_ing: Symbol(),

  /**
   * 已停止
   */
  stoped: Symbol(),

  /**
   * 已完成
   */
  finished: Symbol(),
};

/**
 * 生成文件状态
 * @param {File} file 要上传的文件
 */
function make_item_upload_state(file) {
  const { size, name } = file;

  return {
    /**
     * 文件名称
     */
    file_name: name,

    /**
     * 上传进度
     */
    progress: 0,

    /**
     * 上传状态
     */
    upload_state: upload_state.upload_ing,

    /**
     * 总大小
     */
    total_size: size,

    /**
     * 已上传的大小
     */
    upload_size: 0,

    async start() {
      /**
       * 上传对象实例
       */
      const upload_instance_ref = await upload_bid_file(file);
      return upload_instance_ref.start();
    }, 
  };
}

make_item_upload_state(new File(['abc'],'aa.bb'));

/**
 * 生成文件项
 */
function make_item_arr() {
  return {
    /**
     * 存放原始file对象
     */
    files: [],

    /**
     * 存放上传状态
     */
    status: [
      {
        /**
         * 文件名称
         */
        file_name: "xxxx.jpg",

        /**
         * 上传进度
         */
        progress: 0,

        /**
         * 上传状态
         */
        upload_state: upload_state.upload_ing,

        /**
         * 总大小
         */
        total_size: 0,

        /**
         * 已上传的大小
         */
        upload_size: 0,
      },
    ],
  };
}
export default {
  props: {
    title: {
      type: String,
      default: "文件上传",
    },
    multiple: Boolean,
  },
  data() {
    return {
      files: [make_item_arr()],
      values: [],
      uploading: false,
      upload_state,
    };
  },
  mounted() {
    window.upload_bid_file=upload_bid_file;
  },
  methods: {
    cancel() {
      this.$emit("close");
    },
    success() {
      this.$emit("success", this.values);
    },
  },
};
</script>

 