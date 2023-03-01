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
          :multiple="multiple"
          show-size
          small-chips
          truncate-length="15"
          dene
          v-model="arr_files.files"
          :disabled="uploading"
        ></v-file-input>
        <v-layout
          v-for="(state, stateIndex) in arr_files.status"
          :key="stateIndex"
          wrap
        >
          <v-flex xs12>
            <v-subheader
              >{{ state.file_name }}:
              {{ formatter_file_size(state.upload_size) }}/{{
                formatter_file_size(state.total_size)
              }}

              <template v-if="state.upload_state == upload_states.finished">
                <strong>;上传成功！</strong>
              </template>
            </v-subheader>
          </v-flex>

          <template v-if="state.upload_state != upload_states.finished">
            <v-flex xs10>
              <v-progress-linear :value="state.progress" height="25" bottom>
                <strong style="color: #fff">{{ state.progress }}%</strong>
              </v-progress-linear>
            </v-flex>
            <v-flex xs2>
              <v-btn
                small
                text
                v-if="state.upload_state == upload_states.fail"
                @click="state.start"
              >
                <v-icon color="primary">play_arrow</v-icon>重试
              </v-btn>
              <v-btn
                text
                small
                loading
                v-else-if="state.upload_state == upload_states.upload_ing"
              >
              </v-btn>
            </v-flex>
          </template>
        </v-layout>
      </div>
      <v-btn
        v-if="!uploading && multiple"
        color="primary"
        text
        @click="addInput"
        >添加组</v-btn
      >
    </v-card-text>
    <v-card-actions>
      <v-btn text @click="cancel" color="primary">关闭</v-btn>
      <v-spacer></v-spacer>
      <v-btn
        color="primary"
        :disabled="!files.some((v) => v.files.length > 0) || uploading"
        @click="startUpload"
        >开始上传</v-btn
      >
    </v-card-actions>
  </v-card>
</template>

<script>
import { upload_bid_file } from "./file_splice";
import { formatter_file_size, mapMany } from "../../utils";
import message from ".";
/**
 * 定义上传状态
 */
const upload_states = {
  /**
   * 上传中
   */
  upload_ing: "UPLOADING",

  /**
   * 失败
   */
  fail: "FAIL",

  /**
   * 已完成
   */
  finished: "FINISHED",
};

/**
 * 生成文件状态
 * @param {File} file 要上传的文件
 * @param {Function} on_finished 上传完成的回调
 */
function make_item_upload_state(file, on_finished) {
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
    upload_state: upload_states.upload_ing,

    /**
     * 总大小
     */
    total_size: size,

    /**
     * 已上传的大小
     */
    upload_size: 0,

    /**
     * 上传的结果
     */
    upload_result: null,

    /**
     * 上传对象的实例
     */
    upload_instance_ref: null,

    /**
     * 上传完成回调
     */
    on_finished,

    /**
     * 开始上传
     */
    async start() {
      try {
        if (!this.upload_instance_ref) {
          this.upload_instance_ref = await upload_bid_file(file, {
            on_progress: (total_size, upload_size) => {
              this.upload_size = upload_size;
              this.progress = (
                Math.min(1, upload_size / total_size) * 100
              ).toFixed(2);
            },
          });
        }
        this.upload_result = await this.upload_instance_ref.start();
        this.upload_state = upload_states.finished;

        on_finished && on_finished();
      } catch (error) {
        this.upload_state = upload_states.fail;
      }
    },
  };
}

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
    status: [],
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
      uploading: false,
      upload_states,
    };
  },
  mounted() {
    window.upload_bid_file = upload_bid_file;
  },
  methods: {
    formatter_file_size,
    addInput() {
      this.files.push(make_item_arr());
    },
    startUpload() {
      this.uploading = true;

      for (const arr of this.files) {
        for (const f of arr.files) {
          const file_state = make_item_upload_state(f, this.try_check_success);
          file_state.start();
          arr.status.push(file_state);
        }
      }
    },
    async cancel() {
      if (this.files.some((v) => v.files.length > 0))
        await message.confirm({
          title: "提示",
          content: "确认要取消吗？",
        });
      this.$emit("close");
    },
    try_check_success() {
      setTimeout(() => {
        if (
          this.files.every((v) =>
            v.status.every((x) => x.upload_state == upload_states.finished)
          )
        ) {
          const arr = mapMany(
            this.files.map((v) => v.status.map((x) => x.upload_result))
          );

          this.$emit("success", arr);
        }
      }, 200);
    },
    success() {
      this.$emit("success", this.values);
    },
  },
};
</script>

 