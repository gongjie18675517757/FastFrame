<template>
  <v-card>
    <v-toolbar flat dense color="transparent">
      <v-toolbar-title>流程设计</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn icon @click="zoom -= 10" title="缩小">
        <v-icon color="p">zoom_out</v-icon></v-btn
      >
      {{ zoom }}%
      <v-btn icon @click="zoom += 10" title="放大">
        <v-icon color="p">zoom_in</v-icon>
      </v-btn>

      <v-btn icon @click="$emit('close')" title="关闭" v-if="isDialog">
        <v-icon>close</v-icon>
      </v-btn>
      <v-btn icon @click="handleFullScreen" title="最大化" v-else>
        <v-icon>mdi-fullscreen</v-icon>
      </v-btn>
    </v-toolbar>
    <v-divider></v-divider>
    <v-card-text
      style="padding:0px;"
      @mousedown="handleMousedown"
      ref="container"
      :style="{ cursor: moveing ? 'move' : null }"
    >
      <VuePerfectScrollbar class="flow-design-container" :style="{height:isDialog?'calc(100vh - 50px)':null}">
        <!-- <div class="zoom">
          <v-icon @click="zoom -= 10" color="p">zoom_out</v-icon>
          {{ zoom }}%
          <v-icon @click="zoom += 10" color="p">zoom_in</v-icon>
        </div> -->
        <div
          class="flow-design-areas"
          :style="{
            transform: `scale(${zoom / 100})`,
            top: `${top}px`,
            left: `${left}px`
          }"
        >
          <flow-node
            v-for="(node, nodeIndex) in nodes"
            :key="nodeIndex"
            :node="node"
            :makeNodeFunc="makeNodeFunc"
            editabled
            @add-note="handleNodeAdd($event, nodeIndex)"
            @remove-node="handleNodeRemove(nodeIndex)"
            @change="handleChange"
            @move-up="handleMoveUp(nodeIndex)"
            @move-down="handleMoveDown(nodeIndex)"
            :moveupable="
              nodeIndex > 0 && ['check'].includes(nodes[nodeIndex - 1].nodeType)
            "
            :movedownable="
              nodeIndex < nodes.length - 1 &&
                ['check'].includes(nodes[nodeIndex + 1].nodeType)
            "
          />
        </div>
      </VuePerfectScrollbar>
    </v-card-text>
  </v-card>
</template>

<script>
import VuePerfectScrollbar from "vue-perfect-scrollbar";
import FlowNode from "./FlowNode.vue";

export default {
  components: {
    VuePerfectScrollbar,
    FlowNode
  },
  props: {
    isDialog: Boolean,
    value: {
      type: Array
    }
  },
  data() {
    return {
      zoom: 100,
      top: 30,
      left: 0,
      moveing: false,
      list: [
        {
          nodeType: "start"
        },
        {
          nodeType: "branch",
          branchs: [
            {
              weight: 1,
              isDefault: false,
              nodes: [
                {
                  nodeType: "cond",
                  title: "条件1",
                  filters: [
                    {
                      Name: null,
                      Value: null,
                      Compare: null
                    }
                  ]
                },
                {
                  nodeType: "check",
                  title: "审批人1"
                },
                {
                  nodeType: "check",
                  title: "审批人3"
                }
              ]
            },
            {
              weight: 0,
              isDefault: true,
              nodes: [
                {
                  nodeType: "cond",
                  title: "默认条件",
                  filters: []
                },
                {
                  nodeType: "check",
                  title: "审批人2"
                }
              ]
            }
          ]
        },
        {
          nodeType: "check",
          title: "审批人1"
        },
        {
          nodeType: "end"
        }
      ]
    };
  },
  computed: {
    nodes() {
      return this.value || this.list;
    }
  },
  methods: {
    handleChange() {},
    handleNodeAdd(type, index) {
      let node = this.makeNodeFunc(type);
      this.nodes.splice(index + 1, 0, node);
      this.handleChange();
    },
    makeNodeFunc(type) {
      switch (type) {
        case "check":
          return {
            nodeType: "check",
            title: "审批人"
          };

        case "branch":
          return {
            nodeType: "branch",
            branchs: [
              {
                weight: 1,
                isDefault: false,
                nodes: [
                  {
                    nodeType: "cond",
                    title: "条件1",
                    filters: [
                      {
                        Name: null,
                        Value: null,
                        Compare: null
                      }
                    ]
                  },
                  {
                    nodeType: "check",
                    title: "审批人1"
                  },
                  {
                    nodeType: "check",
                    title: "审批人3"
                  }
                ]
              },
              {
                weight: 0,
                isDefault: true,
                nodes: [
                  {
                    nodeType: "cond",
                    title: "默认条件",
                    filters: []
                  },
                  {
                    nodeType: "check",
                    title: "审批人2"
                  }
                ]
              }
            ]
          };

        default:
          return null;
      }
    },
    handleNodeRemove(index) {
      this.nodes.splice(index, 1);
    },
    handleMoveUp(index) {
      let [r] = this.nodes.splice(index, 1);
      this.nodes.splice(index - 1, 0, r);
    },
    handleMoveDown(index) {
      let [r] = this.nodes.splice(index, 1);
      this.nodes.splice(index + 1, 0, r);
    },
    handleMousedown() {
      this.moveing = true;
      this.$refs.container.addEventListener("mousemove", this.handleMousemove);
      this.$refs.container.addEventListener("mouseup", this.handleMouseup);
    },
    handleMouseup() {
      this.moveing = false;
      this.$refs.container.removeEventListener(
        "mousemove",
        this.handleMousemove
      );
      this.$refs.container.removeEventListener("mouseup", this.handleMouseup);
    },
    handleMousemove(e) {
      this.left += e.movementX;
      this.top += e.movementY;
    },
    handleFullScreen() {
      this.$message.dialog(() => import("./FlowDesign.vue"), {
        fullscreen: true,
        hideOverlay: true,
        value: this.nodes
      });
    }
  }
};
</script>

<style lang="stylus">
.flow-design-container {
  background: #e9eaeb;
  position: relative;
  overflow: auto; 
  padding: 12px;

  .list-enter-active, .list-leave-active {
    transition: all 1s;
  }

  .list-enter, .list-leave-to {
    opacity: 0;
    transform: translateY(30px);
  }

  .zoom {
    position: fixed;
    right: 50px;
    z-index: 10;
  }

  .flow-design-areas {
    // position: absolute;
    // top: 30px;
    // left: 0px;
    user-select: none;

    .node-condition.node-box {
      display: inline-flex;
      flex-direction: column;
      box-shadow: 0 1px 8px 0 rgb(0 0 0 / 10%);
      border-radius: 4px;
      margin-top: 50px;
      margin-right: 50px;
      margin-left: 50px;
    }

    .node {
      width: 100%;
      display: inline-flex;
      flex-direction: column;
      width: 100%;
      align-items: center;
      position: relative;

      .branch-wrap {
        display: inline-flex;
        width: 100%;
        justify-content: center;
        background: #e9eaeb;

        .branch-wrap-box {
          display: flex;
          position: relative;

          .branch-box {
            display: flex;
            min-height: 180px;
            margin-top: 15px;
            border-top: 2px solid #ababab;
            border-bottom: 2px solid #ababab;

            .add-branch {
              width: 84px;
              height: 34px;
              background-color: #fff;
              position: absolute;
              left: 50%;
              transform: translateX(-50%);
              top: 0;
              border-radius: 4px;
              font-size: 14px;
              color: #4A94FF;
              text-align: center;
              line-height: 34px;
              z-index: 21;
              font-family: PingFangSC-Medium;
              cursor: pointer;
              user-select: none;
            }

            .add-branch:hover {
              box-shadow: 0 1px 8px 0 rgb(0 0 0 / 20%);
            }

            .col-box {
              display: inline-flex;
              align-items: center;
              flex-direction: column;
              position: relative;
              padding: 0 20px;

              .top-left-cover-line {
                top: -3px;
                left: -1px;
                height: 4px;
                width: 50%;
                background-color: rgb(233, 234, 235);
                position: absolute;
              }

              .bottom-left-cover-line {
                bottom: -3px;
                left: -1px;
                height: 4px;
                width: 50%;
                background-color: rgb(233, 234, 235);
                position: absolute;
              }

              .top-right-cover-line {
                top: -3px;
                right: -1px;
                height: 4px;
                width: 50%;
                background-color: rgb(233, 234, 235);
                position: absolute;
              }

              .bottom-right-cover-line {
                bottom: -3px;
                right: -1px;
                height: 4px;
                width: 50%;
                background-color: rgb(233, 234, 235);
                position: absolute;
              }
            }

            .col-box::before {
              content: '';
              position: absolute;
              top: 0;
              left: 0;
              right: 0;
              bottom: 0;
              z-index: 0;
              margin: auto;
              width: 2px;
              height: 100%;
              background-color: #ababab;
            }
          }
        }
      }

      .add-node:before {
        content: '';
        position: absolute;
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
        z-index: -1;
        margin: auto;
        width: 2px;
        height: 100%;
        background-color: #ababab;
      }

      .add-node {
        width: 240px;
        display: inline-flex;
        flex-shrink: 0;
        position: relative;

        .add-node-btn {
          user-select: none;
          width: 240px;
          padding: 30px 0;
          display: flex;
          justify-content: center;
          flex-shrink: 0;
          flex-grow: 1;

          .btn:hover {
            background-color: #8599AA;
            box-shadow: 0 1px 8px 0 #8599AA;
          }

          .btn {
            position: relative;
            outline: 0;
            width: 30px;
            height: 30px;
            background-color: #A0B5C8;
            border-radius: 50%;
            line-height: 30px;
            cursor: pointer;

            .btn-icon:before {
              content: '';
              position: absolute;
              display: inline-block;
              width: 2px;
              height: 16px;
              left: 14px;
              top: 7px;
              background-color: #fff;
            }

            .btn-icon:after {
              content: '';
              position: absolute;
              display: inline-block;
              height: 2px;
              width: 16px;
              top: 14px;
              left: 7px;
              background-color: #fff;
            }
          }
        }
      }

      .node-box {
        position: relative;
        display: flex;
        flex-direction: column;
        background: #FFF;
        width: 240px;
        min-height: 92px;
        -webkit-box-sizing: border-box;
        box-sizing: border-box;
        cursor: pointer;
        z-index: 10;

        &.node-start {
          color: #4A94FF;
        }

        &.node-start:hover {
          box-shadow: 0 1px 8px 0 #4a94ff;
        }

        &.node-end:hover {
          box-shadow: 0 1px 8px 0 green;
        }

        &.node-check:hover {
          box-shadow: 0 1px 8px 0 #FCAD22;
        }

        &.node-cond:hover {
          box-shadow: 0 1px 8px 0 #88939F;
        }

        &.node-start, &.node-end, &.node-check, &.node-cond {
          display: flex;
          align-items: center;
          justify-content: center;
          width: 240px;
          min-height: 56px;
          height: 56px;
          box-shadow: 0 1px 8px 0 #ababab;
          cursor: default;
          font-size: 15px;
        }

        &.node-check, &.node-cond {
          height: 92px;
          cursor: pointer;
        }

        &.node-check:after {
          background: #FCAD22;
          content: '';
          position: absolute;
          display: inline-block;
          height: 4px;
          width: 100%;
          top: 0;
          left: 0;
          border-radius: 2px 2px 0 0;
        }

        &.node-cond:after {
          background: #88939F;
          content: '';
          position: absolute;
          display: inline-block;
          height: 4px;
          width: 100%;
          top: 0;
          left: 0;
          border-radius: 2px 2px 0 0;
        }

        &.node-start:after {
          background: #4A94FF;
          content: '';
          position: absolute;
          display: inline-block;
          height: 4px;
          width: 100%;
          top: 0;
          left: 0;
          border-radius: 2px 2px 0 0;
        }

        &.node-end:after {
          background: green;
          content: '';
          position: absolute;
          display: inline-block;
          height: 4px;
          width: 100%;
          top: 0;
          left: 0;
          border-radius: 2px 2px 0 0;
        }
      }
    }

    .line-end-arrow {
      position: absolute;
      top: -8px;
      left: 50%;
      transform: translateX(-50%);
      width: 0;
      height: 4px;
      border-style: solid;
      border-width: 8px 5px 4px;
      border-color: #ababab transparent transparent;
    }
  }
}
</style>