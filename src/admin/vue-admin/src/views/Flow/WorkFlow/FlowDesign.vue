<template>
  <v-card tile>
    <v-toolbar flat dense height="30px">
      <v-toolbar-title>{{title}}</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn icon small @click="handlePlusCanvas">
        <v-icon>mdi-magnify-plus-outline</v-icon>
      </v-btn>
      {{canvasDataRoom}}%
      <v-btn icon small @click="handleMinusCanvas">
        <v-icon>mdi-magnify-minus-outline</v-icon>
      </v-btn>
    </v-toolbar>
    <v-card-text>
      <v-layout>
        <v-flex xs12>
          <div class="flow-layout">
            <div class="flow-editor">
              <div class="canvas-container" :data-zoom="canvasDataRoom">
                <div
                  class="campaignCanvas"
                  :style="canvasRoomScaleStyle"
                  v-if="libLoad"
                  ref="container"
                >
                  <Node
                    v-for="v in nodes"
                    :key="v.id"
                    v-bind="v"
                    :ref="v.id"
                    @nodeMoved="handleNodeMoved(v,$event)"
                  >{{v.text}}</Node>
                </div>
              </div>
            </div>
          </div>
        </v-flex>
      </v-layout>
    </v-card-text>
    <!-- <RemoteJS :src="libSrc" @load="handleLibLoad" /> -->
  </v-card>
</template>
 
 
<script>
import { jsPlumb } from "jsplumb";
// import VueDraggableResizable from "vue-draggable-resizable";
// import "vue-draggable-resizable/dist/VueDraggableResizable.css";

const NodeTypeEnum = {
  Start: "Start",
  Node: "Node",
  End: "End",
  Template: "Template"
};

   

const Node = {
  props: {
    id: Number,
    type: {
      type: String,
      default: NodeTypeEnum.Node
    },
    next: Array,
    prev: Array,
    x: Number,
    y: Number
  },
  inject: ["getContainer", "getJsPlumbInstance", "connectNode"],
  data() {
    return {};
  },
  computed: {
    plumbInstance() {
      return this.getJsPlumbInstance();
    },
    flowStyle() {
      let comm = {
        display: "flex",
        "align-items": "center",
        "justify-content": "center",
        "text-align": "justify",
        position: "absolute",
        cursor: "default",
        color: "white",
        "font-weight": "bolder",
        "font-size": "16px",
        left: `${this.x}px`,
        top: `${this.y}px`
      };
      switch (this.type) {
        case NodeTypeEnum.Start:
          return {
            ...comm,
            width: `60px`,
            height: `60px`,
            "border-radius": `30px`,
            background: "#dc3c00",
            border: `1px solid #dc3c00`
          };
        case NodeTypeEnum.End:
          return {
            ...comm,
            width: `60px`,
            height: `60px`,
            "border-radius": `30px`,
            background: "#dc3c00",
            border: `1px solid #dc3c00`
          };
        case NodeTypeEnum.Template:
          return {
            ...comm,
            width: `60px`,
            height: `60px`,
            "border-radius": `30px`,
            background: "#dc3c00",
            border: `1px solid #dc3c00`
          };
        case NodeTypeEnum.Node:
          return {
            ...comm,
            width: `150px`,
            height: `50px`,
            "border-radius": `30px`,
            background: "#dc3c00",
            border: `1px solid #dc3c00`
          };
        default:
          break;
      }
    }
  },
  mounted() {
    let containerEl = this.getContainer();
    let $el = this.$el;

    /**
     * 创建拖动
     */
    this.plumbInstance.draggable($el, {
      containment: containerEl,
      grid: [5, 5],
      stop: ({ pos: [x, y] }) => {
        this.$emit("nodeMoved", {
          x,
          y
        });
      }
    });

    /**
     * 创建连线
     */
    for (const n of this.next) {
      this.connectNode(this.id, n);
    }
  },
  render(h) {
    return h(
      "div",
      {
        style: this.flowStyle
      },
      this.$slots.default
    );
  }
};

export default {
  components: {
    Node
  },
  props: {
    title: String
  },
  provide() {
    return {
      getContainer: () => this.$refs.container,
      getJsPlumbInstance: () => this.jsPlumbInstance,
      connectNode: this.connectNode
    };
  },
  data() {
    return {
      canvasDataRoom: 100,
      libLoad: false,
      jsPlumbInstance: null,

      nodes: [
        {
          id: 0,
          x: 100,
          y: 200,
          type: NodeTypeEnum.Start,
          text: "起点",
          next: [-2],
          prev: []
        },
        {
          id: -2,
          x: 200,
          y: 200,
          type: NodeTypeEnum.Template,
          text: "模板",
          next: [],
          prev: [0]
        },

        {
          id: 1,
          x: 250,
          y: 50,
          type: NodeTypeEnum.Node,
          text: "节点"
        },
        {
          id: -1,
          x: 500,
          y: 50,
          type: NodeTypeEnum.End,
          text: "终点"
        }
      ]
    };
  },

  computed: {
    canvasRoomMinusEnable() {
      return this.canvasDataRoom > 50;
    },
    canvasRoomPlusEnable() {
      return this.canvasDataRoom < 100;
    },
    canvasRoomScaleStyle() {
      return {
        transform: "scale(" + this.canvasDataRoom / 100 + ")"
      };
    }
  },
  mounted() {
    const instance = jsPlumb.getInstance();

    instance.ready(() => {
      instance.addEndpoint;
      instance.setContainer(this.$refs.container);
      instance.importDefaults({
        ConnectionsDetachable: false,
        LogEnabled: true
      });
      this.libLoad = true;
    });
    this.jsPlumbInstance = instance;
  },
  methods: {
    connectNode(s, t) {
      this.$nextTick(() => {
        const getEl = k => {
          let refs = this.$refs[k];

          if (Array.isArray(refs)) {
            return refs[0].$el;
          } else if (typeof refs == "object") {
            return refs.$el;
          } else {
            return null;
          }
        };
        let source = getEl(s);
        let target = getEl(t);
        if (!source || !target) return;

        let config = {
          source,
          target,
          endpoint: "Dot",
          // 连接线的样式
          connectorStyle: {
            strokeStyle: "#ccc",
            joinStyle: "round",
            outlineColor: "#ccc"
          }, // 链接 style
          // 连接线配置，起点可用
          connector: [
            "Flowchart",
            {
              stub: [10, 20],
              gap: 1,
              cornerRadius: 2,
              alwaysRespectStubs: true
            }
          ], //  链接
          //
          endpointStyle: {
            fill: "transparent",
            outlineStroke: "transparent",
            outlineWidth: 2
          },
          // 线的样式
          paintStyle: { stroke: "lightgray", strokeWidth: 2 },
          // 锚点的位置
          anchor: ["LeftMiddle", "RightMiddle", "BottomCenter", "TopCenter"],
          // 遮罩层-设置箭头
          overlays: [
            ["PlainArrow", { width: 10, length: 10, location: 1 }],
            [
              "Custom",
              {
                location: 0.5,
                id: "nodeTempSmall",
                create: function() {
                  let $el = target;
                  $el.dataset.target = target;
                  $el.dataset.source = source;
                  return $el;
                },
                visible: false
              }
            ],
            [
              "Label",
              {
                location: 1,
                id: "flowItemDesc",
                cssClass: "node-item-label",
                visible: true
              }
            ] //
          ]
        };

        this.jsPlumbInstance.connect(config);
      });
    },
    handleMinusCanvas() {
      this.canvasDataRoom = this.canvasDataRoom - 10;
    },
    handlePlusCanvas() {
      this.canvasDataRoom = this.canvasDataRoom + 10;
    },
    handleNodeMoved(v, { x, y }) {
      v.x = x;
      v.y = y;
    }
  }
};
</script>
<style scoped>
.flow-layout {
  display: flex;
  flex-direction: column;
  height: 50vh;
}

.flow-editor {
  position: relative;
  display: flex;
  flex-direction: row;
  flex: 1;
  overflow: hidden;
}

.canvas-container {
  flex: 1;
  overflow: auto;
  z-index: 0;
}

.canvas-container:before {
  content: "";
  height: 10px;
  width: 100%;
  display: block;
  background-repeat: repeat-x;
  position: absolute;
  background-image: linear-gradient(90deg, #ccc 1px, transparent 0),
    linear-gradient(90deg, #ddd 1px, transparent 0);
  background-size: 75px 10px, 5px 5px;
}

.canvas-container:after {
  content: "";
  height: 100%;
  width: 10px;
  display: block;
  background-repeat: repeat-y;
  position: absolute;
  top: 0;
  background-image: linear-gradient(#ccc 1px, transparent 0),
    linear-gradient(#ddd 1px, transparent 0);
  background-size: 10px 75px, 5px 5px;
}

.canvas-container[data-zoom="100"] {
  background-image: linear-gradient(#eee 1px, transparent 0),
    linear-gradient(90deg, #eee 1px, transparent 0),
    linear-gradient(#f5f5f5 1px, transparent 0),
    linear-gradient(90deg, #f5f5f5 1px, transparent 0);
  background-size: 75px 75px, 75px 75px, 15px 15px, 15px 15px;
}

.canvas-container[data-zoom="90"] {
  background-image: linear-gradient(#eee 1px, transparent 0),
    linear-gradient(90deg, #eee 1px, transparent 0),
    linear-gradient(#f5f5f5 1px, transparent 0),
    linear-gradient(90deg, #f5f5f5 1px, transparent 0);
  background-size: 70px 70px, 70px 70px, 14px 14px, 14px 14px;
}

.canvas-container[data-zoom="80"] {
  background-image: linear-gradient(#eee 1px, transparent 0),
    linear-gradient(90deg, #eee 1px, transparent 0),
    linear-gradient(#f5f5f5 1px, transparent 0),
    linear-gradient(90deg, #f5f5f5 1px, transparent 0);
  background-size: 60px 60px, 60px 60px, 12px 12px, 12px 12px;
}

.canvas-container[data-zoom="70"] {
  background-image: linear-gradient(#eee 1px, transparent 0),
    linear-gradient(90deg, #eee 1px, transparent 0),
    linear-gradient(#f5f5f5 1px, transparent 0),
    linear-gradient(90deg, #f5f5f5 1px, transparent 0);
  background-size: 55px 55px, 55px 55px, 11px 11px, 11px 11px;
}

.canvas-container[data-zoom="60"] {
  background-image: linear-gradient(#eee 1px, transparent 0),
    linear-gradient(90deg, #eee 1px, transparent 0),
    linear-gradient(#f5f5f5 1px, transparent 0),
    linear-gradient(90deg, #f5f5f5 1px, transparent 0);
  background-size: 45px 45px, 45px 45px, 9px 9px, 9px 9px;
}

.canvas-container[data-zoom="50"] {
  background-image: linear-gradient(#eee 1px, transparent 0),
    linear-gradient(90deg, #eee 1px, transparent 0),
    linear-gradient(#f5f5f5 1px, transparent 0),
    linear-gradient(90deg, #f5f5f5 1px, transparent 0);
  background-size: 40px 40px, 40px 40px, 8px 8px, 8px 8px;
}
.campaignCanvas {
  background-color: transparent;
  transform-origin: top;
  width: 100%;
  height: 100%;
  overflow: visible !important;
}
</style>
