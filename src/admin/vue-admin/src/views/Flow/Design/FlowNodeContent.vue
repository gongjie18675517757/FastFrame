<template>
  <div style="width:100%;" @click="$emit('node-selected')">
    <div class="title">
      <span class="node-title-name">{{ title }}
        <span v-if="NodeEnum=='cond' && weight">
          (权重:{{weight}})
        </span>
      </span>
      <span class="node-title-name-editable">
        <v-menu
          offset-y
          class="node-title-delete"
          :close-on-content-click="false"
          v-model="editInputVisible"
          v-if="!readonly && editabled"
        >
          <template v-slot:activator="{ on, attrs }">
            <i
              class="ww_icon ww_approvalFlowIcon_Editable"
              v-bind="attrs"
              v-on="on"
            ></i>
          </template>
          <v-card>
            <v-card-text>
              <v-text-field
                v-model="titleTemp"
                hide-details
                label="节点名称"
              ></v-text-field>
            </v-card-text>
            <v-card-actions>
              <v-btn small text @click="editInputVisible = false">
                取消
              </v-btn>
              <v-spacer></v-spacer>
              <v-btn color="p" small text @click="handleTitleInput">确认</v-btn>
            </v-card-actions>
          </v-card>
        </v-menu>
      </span>
      <span class="node-title-operate" style="flex-grow: 1;">
        <v-menu v-if="!readonly && editabled" offset-y class="node-title-delete" v-model="delMenuVisible">
          <template v-slot:activator="{ on, attrs }">
            <i
              class="ww_icon ww_approvalFlowIcon_Close"
              v-bind="attrs"
              v-on="on"
            ></i>
          </template>
          <v-card>
            <v-card-text style="padding:12px;">
              确认要删除此{{NodeEnum=='cond'?'条件分支':'节点'}}吗?
              <v-spacer></v-spacer>
            </v-card-text>
            <v-card-actions>
              <v-btn small text @click="delMenuVisible = false">
                取消
              </v-btn>
              <v-spacer></v-spacer>
              <v-btn color="p" small text @click="$emit('remove-node')"
                >确认</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-menu>
      </span>
    </div>
    <div class="content">
      <div class="content-text">
        <div>
          <div class="content-text-default">{{ text | substring(17)}}</div>
        </div>
      </div>
      <i v-if="!readonly && editabled" class="ww_commonImg ww_commonImg_PageNavArrowRightDisabled"></i>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    title: String,
    NodeEnum: String,
    placeholder: String,
    readonly: Boolean,
    editabled: Boolean,
    weight:[Number,String]
  },
  data() {
    return {
      delMenuVisible: false,
      editInputVisible: false,
      titleTemp: null
    };
  },
  watch: {
    editInputVisible(val) {
      if (val) {
        this.titleTemp = this.title;
      }
    }
  },
  computed: {
    text() {
      if (this.placeholder) {
        return this.placeholder;
      }
      switch (this.NodeEnum) {
        case "check":
          return "请选择审核人";

        case "cond":
          return "请设置条件";

        default:
          break;
      }

      return null;
    }
  },
  methods: {
    handleTitleInput() {
      this.editInputVisible = false;
      this.$emit("update:title", this.titleTemp);
    }
  }
};
</script> 