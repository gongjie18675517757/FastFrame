<template>
  <div class="node" v-if="node">
    <template v-if="node.NodeEnum == 'start'">
      <div class="node-box node-start">流程提交</div>
    </template>
    <template v-else-if="node.NodeEnum == 'end'">
      <div class="node-box node-end">流程完结</div>
    </template>
    <template v-else-if="node.NodeEnum == 'check'">
      <div class="node-box node-check">
        <span
          class="node-move-up-icon node-move-up-icon-disable"
          v-if="moveupable && !readonly"
          @click="$emit('move-up')"
        ></span>
        <span
          class="node-move-down-icon node-move-down-icon-disable"
          v-if="movedownable && !readonly"
          @click="$emit('move-down')"
        ></span>
        <div class="line-end-arrow"></div>
        <v-node-content
          :title.sync="node.Title"
          :NodeEnum="node.NodeEnum"
          :readonly="readonly"
          :editabled="editabled"
          @remove-node="$emit('remove-node')"
          @node-selected="$emit('node-selected', node)"
        />
      </div>
    </template>
    <template v-else-if="node.NodeEnum == 'cond'">
      <div class="node-condition">
        <div class="node-box node-cond node-condition">
          <div class="line-end-arrow"></div>
          <v-node-content
            :title.sync="node.Title"
            :NodeEnum="node.NodeEnum"
            :readonly="readonly"
            :editabled="editabled"
            :placeholder="node.IsDefault?'缺省条件':null"
            @remove-node="$emit('remove-node')"
            @node-selected="$emit('node-selected', node)"
          />
        </div>
      </div>
    </template>
    <template v-else-if="node.NodeEnum == 'branch'">
      <div class="branch-wrap">
        <div class="branch-wrap-box">
          <div class="branch-box">
            <div class="add-branch" v-if="readonly">条件分支</div>
            <div class="add-branch" v-else @click="addBranch">添加条件</div>
            <div
              class="col-box"
              v-for="(branch, branchIndex) in node.Nodes"
              :key="branchIndex"
            >
              <template v-if="branchIndex == 0">
                <div class="top-left-cover-line"></div>
                <div class="bottom-left-cover-line"></div>
              </template>
              <template v-if="branchIndex == node.Nodes.length - 1">
                <div class="top-right-cover-line"></div>
                <div class="bottom-right-cover-line"></div>
              </template>

              <v-self
                v-for="(childNode, childNodeIndex) in branch.Nodes"
                :key="`${branchIndex}_${childNodeIndex}`"
                :node="childNode"
                @add-note="handleNodeAdd($event, childNodeIndex, branch.Nodes)"
                @remove-node="handleNodeRemove(childNodeIndex, branchIndex)"
                @node-selected="$emit('node-selected', $event)"
                @change="$emit('change')"
                @move-up="handleMoveUp(childNodeIndex, branchIndex)"
                @move-down="handleMoveDown(childNodeIndex, branchIndex)"
                :makeNodeFunc="makeNodeFunc"
                :moveupable="
                  childNodeIndex > 0 &&
                    ['check'].includes(
                      branch.Nodes[childNodeIndex - 1].NodeEnum
                    )
                "
                :movedownable="
                  childNodeIndex < branch.Nodes.length - 1 &&
                    ['check'].includes(
                      branch.Nodes[childNodeIndex + 1].NodeEnum
                    )
                "
                :editabled="
                  childNode.NodeEnum != 'cond' ||
                    branchIndex != node.Nodes.length - 1
                "
                :readonly="readonly"
              />
            </div>
          </div>
        </div>
      </div>
    </template>
    <template v-if="!['start', 'cond'].includes(node.NodeEnum)">
      <div class="line-end-arrow"></div>
    </template>
    <template v-if="!['end'].includes(node.NodeEnum)">
      <div class="add-node">
        <div
          class="add-node-menu-wrap add-node-menu-right"
          v-if="addMenuVisible"
          ref="add-menu"
        >
          <div class="add-node-menu">
            <div
              v-for="r in addMenuList"
              :key="r.Key"
              :class="['menu-item', `menu-${r.icon}`]"
              @click="handleAddMenuClick(r)"
            >
              <i
                :class="[
                  'ww_icon',
                  `ww_approvalFlowIcon_${r.icon}`,
                  'menu-item-icon'
                ]"
              ></i>
              <div>{{ r.Value }}</div>
            </div>
          </div>
        </div>
        <div class="add-node-btn">
          <!-- @click="addNode(nodes, nodeIndex, 'check')" -->
          <div
            class="btn"
            v-if="!readonly"
            @click.stop="addMenuVisible = !addMenuVisible"
          >
            <span class="btn-icon"></span>
          </div>
        </div>
      </div>
    </template>
  </div>
</template>

<script>
import FlowNodeContent from "./FlowNodeContent.vue";

export default {
  components: {
    "v-self": () => import("./FlowNode.vue"),
    "v-node-content": FlowNodeContent
  },
  props: {
    node: Object,
    readonly: Boolean,
    editabled: Boolean,
    moveupable: Boolean,
    movedownable: Boolean,
    makeNodeFunc: Function
  },
  data() {
    return {
      addMenuList: [
        {
          Key: "check",
          Value: "审批人",
          icon: "approver"
        },
        // {
        //   Key: "notify",
        //   Value: "抄送人",
        //   icon: "notify"
        // },
        {
          Key: "branch",
          Value: "条件分支",
          icon: "branch"
        }
      ],
      addMenuVisible: false,
      delMenuVisible: false,
      editInputVisible: false,
      titleTemp: null
    };
  },
  watch: {
    editInputVisible(val) {
      if (val) {
        this.titleTemp = this.node.Title;
      }
    }
  },
  mounted() {
    window.addEventListener("click", this.tryHideAddMenu);
  },
  beforeDestroy() {
    window.removeEventListener("click", this.tryHideAddMenu);
  },
  methods: {
    tryHideAddMenu(e) {
      if (!this.addMenuVisible) return;

      let $el = this.$refs["add-menu"];
      if (!$el) return;

      for (const el of e.path) {
        if (el == $el) return;
      }

      this.addMenuVisible = false;
    },
    handleAddMenuClick(r) {
      this.addMenuVisible = false;
      this.$emit("add-note", r.Key);
    },
    addBranch() {
      this.node.Nodes.splice(0, 0, {
        Weight: 1,
        IsDefault: false,
        Nodes: [
          {
            NodeEnum: "cond",
            Title: "条件1",
            Conds: [],
            IsDefault: false,
          },
          {
            NodeEnum: "check",
            Title: "审批人1",
            Events: [],
            Checkers: []
          },
          {
            NodeEnum: "check",
            Title: "审批人3",
            Events: [],
            Checkers: []
          }
        ]
      });
      this.$emit("change");
    },
    handleNodeAdd(type, index, nodes) {
      let node = this.makeNodeFunc(type);
      nodes.splice(index + 1, 0, node);
      this.$emit("change");
    },
    handleNodeRemove(index, branchIndex) {
      let branch = this.node.Nodes[branchIndex];
      let node = branch.Nodes[index];
      branch.Nodes.splice(index, 1);

      if (node.NodeEnum == "cond") {
        this.node.Nodes.splice(branchIndex, 1);
        if (this.node.Nodes.length <= 1) this.$emit("remove-node");
      }
      this.$emit("change");
    },
    handleTitleInput() {
      this.node.Title = this.titleTemp;
      this.editInputVisible = false;
      this.$emit("change");
    },
    handleMoveUp(index, branchIndex) {
      let branch = this.node.Nodes[branchIndex];
      let nodes = branch.Nodes;
      let [r] = nodes.splice(index, 1);
      nodes.splice(index - 1, 0, r);
      this.$emit("change");
    },
    handleMoveDown(index, branchIndex) {
      let branch = this.node.Nodes[branchIndex];
      let nodes = branch.Nodes;
      let [r] = nodes.splice(index, 1);
      nodes.splice(index + 1, 0, r);
      this.$emit("change");
    }
  }
};
</script>

<style lang="stylus">
.add-node-menu-wrap {
  position: absolute;
  left: 60%;
  top: 10px;
  z-index: 11;

  .add-node-menu {
    display: inline-flex;
    flex-direction: row;
    -webkit-box-shadow: 0 1px 8px 0 rgb(0 0 0 / 10%);
    box-shadow: 0 1px 8px 0 rgb(0 0 0 / 10%);
    border-radius: 6px;
    overflow: hidden;

    .menu-item {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      font-size: 12px;
      width: 90px;
      min-width: 90px;
      height: 74px;
      margin-right: 1px;
      background-color: #fff;
      cursor: pointer;
      z-index: 10;
    }

    .menu-item-icon {
      display: block;
      margin-bottom: 4px;
    }

    .ww_approvalFlowIcon_approver {
      background-image: url('/images/flow.png');
      background-position: -100px 0;
      width: 30px;
      height: 30px;
    }

    .ww_approvalFlowIcon_notify {
      background-image: url('/images/flow.png');
      background-position: -80px -85px;
      width: 30px;
      height: 30px;
    }

    .ww_approvalFlowIcon_branch {
      background-position: 0 -85px;
    }

    .ww_approvalFlowIcon_BigDelete, .ww_approvalFlowIcon_branch {
      background-image: url('/images/flow.png');
      width: 30px;
      height: 30px;
    }

    .ww_icon {
      display: inline-block;
      overflow: hidden;
      font-size: 0;
      line-height: 0;
    }

    .menu-approver {
      color: #FCAD22;
    }

    .menu-notifier {
      color: #3CB4B2;
    }

    .menu-branch {
      color: #88939F;
    }
  }
}

.add-node-menu-right::before {
  content: '';
  position: absolute;
  display: block;
  width: 8px;
  height: 8px;
  transform: rotate(45deg);
  border-style: solid;
  border-width: 6px;
  border-color: #fff;
  left: -4px;
  top: 30px;
}

.node-box:hover .node-move-down-icon, .node-box:hover .node-move-down-icon-disable, .node-box:hover .node-move-up-icon, .node-box:hover .node-move-up-icon-disable {
  display: inline-block;
}

.node-move-up-icon, .node-move-up-icon-disable {
  top: -20px;
}

.node-move-down-icon-disable, .node-move-up-icon-disable {
  background-image: url('https://wwcdn.weixin.qq.com/node/wework/images/move_up_disable_2x.d3a36fc789.png');
}

.node-move-up-icon, .node-move-up-icon-disable {
  display: none;
  position: absolute;
  left: 0;
  right: 0;
  margin: auto;
  background-repeat: no-repeat;
  background-size: contain;
  width: 32px;
  height: 32px;
  z-index: 10000;
  cursor: pointer;
  user-select: none;
  background-image: url('https://wwcdn.weixin.qq.com/node/wework/images/move_up_normal_2x.eb969e7048.png');
}

.node-box:hover .node-move-down-icon, .node-box:hover .node-move-down-icon-disable, .node-box:hover .node-move-up-icon, .node-box:hover .node-move-up-icon-disable {
  display: inline-block;
}

.node-move-down-icon, .node-move-down-icon-disable {
  bottom: -20px;
  -webkit-transform: rotate(180deg);
  -ms-transform: rotate(180deg);
  transform: rotate(180deg);
}

.node-move-down-icon-disable, .node-move-up-icon-disable {
  background-image: url('https://wwcdn.weixin.qq.com/node/wework/images/move_up_disable_2x.d3a36fc789.png');
}

.node-move-down-icon, .node-move-down-icon-disable, .node-move-up-icon, .node-move-up-icon-disable {
  display: none;
  position: absolute;
  left: 0;
  right: 0;
  margin: auto;
  background-repeat: no-repeat;
  background-size: contain;
  width: 32px;
  height: 32px;
  z-index: 10000;
  cursor: pointer;
  user-select: none;
  background-image: url('https://wwcdn.weixin.qq.com/node/wework/images/move_up_normal_2x.eb969e7048.png');
}

.node-check .title {
  background: #FFF9EE;
  box-shadow: 0 0 0 0 #4a94ff;
  color: #FCAD22;
}

.node-cond .title {
  background: #EEF4FB;
  box-shadow: 0 0 0 0 #4a94ff;
  color: #88939F;
}

.node-box .title {
  position: relative;
  display: flex;
  align-items: center;
  height: 36px;
  border-radius: 4px 4px 0 0;
  font-family: PingFangSC-Medium;
  font-size: 14px !important;
  padding: 0 14px;
  width: 100%;
}

.node-box .content {
  display: flex;
  align-items: center;
  flex-grow: 1;
  padding: 16px 14px;
  width: 100%;
}

.node-box .node-title-name {
  display: inline-block;
  -o-text-overflow: ellipsis;
  text-overflow: ellipsis;
  white-space: nowrap;
  overflow: hidden;
  max-width: 76%;
}

.node-box:hover .node-title-name-editable {
  display: inline-flex;
}

.node-box .node-title-name-editable {
  display: none;
  margin-left: 4px;
}

.ww_approvalFlowIcon_Editable {
  background-position: -140px -86px;
}

.node-box:hover .node-title-operate {
  display: inline-flex;
  margin-top: 2px;
}

.node-box .node-title-operate {
  display: none;
  flex: 1;
  flex-direction: row-reverse;
}

.ww_approvalFlowIcon_Close {
  background-position: -140px -38px;
}

.ww_approvalFlowIcon_Close, .ww_approvalFlowIcon_CopyNode, .ww_approvalFlowIcon_Editable {
  width: 14px;
  height: 14px;
  background-image: url('/images/flow.png');
  display: inline-block;
}

.node-box .content-text {
  font-family: PingFangSC-Regular;
  flex: 1;
  font-size: 14px;
  color: #141414;
  word-break: break-all;
  line-height: 1.8;
}

.ww_commonImg_PageNavArrowRightDisabled {
  background-position: -398px -23px;
}

.ww_commonImg_PageNavArrowLeftDisabled, .ww_commonImg_PageNavArrowLeftHover, .ww_commonImg_PageNavArrowLeftNormal, .ww_commonImg_PageNavArrowRightDisabled, .ww_commonImg_PageNavArrowRightHover, .ww_commonImg_PageNavArrowRightNormal {
  width: 8px;
  height: 13px;
  background-image: url('/images/common.png');
}
</style>