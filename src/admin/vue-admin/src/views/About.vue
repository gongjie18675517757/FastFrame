<template>
  <div class="flow-container">
    <div class="approve-example-container">
      <div class="node-panel">
        <div
          v-for="item in approveNodes"
          :class="[`approve-node node-${item.type}`]"
          :key="item.type"
        >
          <div
            class="node-shape"
            :style="item.style"
            @mousedown="dragNode(item)"
          ></div>
          <div class="node-label">{{ item.label }}</div>
        </div>
      </div>
      <div ref="container" class="viewport"></div>
      <!-- <div id="graph" class="viewport" /> -->

      <div class="property-panel" v-if="nodeData">
        {PropertyPanel(nodeData, updateProperty, hidePropertyPanel)}
      </div>
    </div>
    <!-- <div ref="container" class="flow-container"></div> -->
    <!-- <div ref="vv"></div> -->
  </div>
</template>

<script>
import "@logicflow/core/dist/style/index.css";

import LogicFlow, {
  // BaseNodeModel,
  // ConnectRule,
  CircleNodeModel,
  CircleNode,
  h,
  RectNode,
  RectNodeModel,
  PolygonNode,
  PolygonNodeModel,
} from "@logicflow/core";
// import Vue from "vue";

const themeApprove = {
  rect: {
    // 矩形样式
    radius: 8,
    stroke: "#3CB371",
  },
  circle: {
    r: 25,
    stroke: "#FF6347",
  },
  polygon: {
    stroke: "#6495ED",
  },
  polyline: {
    strokeWidth: 1,
  },
  edgeText: {
    background: {
      fill: "white",
    },
  },
};

const config = {
  stopScrollGraph: true,
  stopZoomGraph: true,
  grid: {
    size: 10,
    visible: true,
    type: "mesh",
    config: {
      color: "#DCDCDC", // 设置网格的颜色
    },
  },
  keyboard: { enabled: true },
  style: themeApprove,
};

const data = {
  nodes: [
    {
      id: "28df2fbe-f32b-4a9b-b544-7e70d7187b33",
      type: "apply",
      x: 210,
      y: 210,
      text: { x: 210, y: 210, value: "申请" },
      properties: {},
    },
    {
      id: "64179bd7-c60e-433c-8df7-97c8e98f855d",
      type: "approver",
      x: 350,
      y: 210,
      text: { x: 350, y: 210, value: "审批" },
      properties: {
        labelColor: "#000000",
        approveTypeLabel: "直接上级",
        approveType: "leader",
      },
    },
    {
      id: "fcb96f10-720e-40e5-8ed0-ebdd0a46f234",
      type: "jugement",
      x: 510,
      y: 210,
      text: { x: 510, y: 210, value: "判断报销是否\n大于1000元" },
      properties: { api: "" },
    },
    {
      id: "9f119df3-c449-4e5d-a67a-cb351b9cbdb5",
      type: "approver",
      x: 670,
      y: 210,
      text: { x: 670, y: 210, value: "审批" },
      properties: {
        labelColor: "#000000",
        approveTypeLabel: "T2领导",
        approveType: "t2Leader",
      },
    },
    {
      id: "ef34f09c-38ea-4ad4-acd2-cc2f464a2be6",
      type: "finsh",
      x: 850,
      y: 210,
      text: { x: 850, y: 210, value: "结束" },
      properties: {},
    },
  ],
  edges: [
    {
      id: "0d87b1eb-2389-445a-9f34-6227940b2072",
      type: "polyline",
      sourceNodeId: "28df2fbe-f32b-4a9b-b544-7e70d7187b33",
      targetNodeId: "64179bd7-c60e-433c-8df7-97c8e98f855d",
      startPoint: { x: 235, y: 210 },
      endPoint: { x: 300, y: 210 },
      text: { x: 51.25, y: 0, value: "" },
      properties: {},
      pointsList: [
        { x: 235, y: 210 },
        { x: 300, y: 210 },
      ],
    },
    {
      id: "d99e7451-b379-411e-b0da-df11be8be20a",
      type: "polyline",
      sourceNodeId: "64179bd7-c60e-433c-8df7-97c8e98f855d",
      targetNodeId: "fcb96f10-720e-40e5-8ed0-ebdd0a46f234",
      startPoint: { x: 400, y: 210 },
      endPoint: { x: 475, y: 210 },
      text: { x: 437.5, y: 210, value: "通过" },
      properties: {},
      pointsList: [
        { x: 400, y: 210 },
        { x: 475, y: 210 },
      ],
    },
    {
      id: "4c615802-15d8-442c-be22-b65430286123",
      type: "polyline",
      sourceNodeId: "fcb96f10-720e-40e5-8ed0-ebdd0a46f234",
      targetNodeId: "9f119df3-c449-4e5d-a67a-cb351b9cbdb5",
      startPoint: { x: 545, y: 210 },
      endPoint: { x: 620, y: 210 },
      text: { x: 582.5, y: 210, value: "是" },
      properties: {},
      pointsList: [
        { x: 545, y: 210 },
        { x: 620, y: 210 },
      ],
    },
    {
      id: "934ae03a-6ee0-4568-a2b4-8bcede565e0b",
      type: "polyline",
      sourceNodeId: "9f119df3-c449-4e5d-a67a-cb351b9cbdb5",
      targetNodeId: "ef34f09c-38ea-4ad4-acd2-cc2f464a2be6",
      startPoint: { x: 720, y: 210 },
      endPoint: { x: 825, y: 210 },
      text: { x: -10, y: 0, value: "" },
      properties: {},
      pointsList: [
        { x: 720, y: 210 },
        { x: 825, y: 210 },
      ],
    },
    {
      id: "bd5e1dd0-1978-46f7-851b-d31c03aebee9",
      type: "polyline",
      sourceNodeId: "64179bd7-c60e-433c-8df7-97c8e98f855d",
      targetNodeId: "ef34f09c-38ea-4ad4-acd2-cc2f464a2be6",
      startPoint: { x: 350, y: 170 },
      endPoint: { x: 850, y: 185 },
      text: { x: 600, y: 140, value: "驳回" },
      properties: {},
      pointsList: [
        { x: 350, y: 170 },
        { x: 350, y: 140 },
        { x: 850, y: 140 },
        { x: 850, y: 185 },
      ],
    },
    {
      id: "453139c3-faa1-4e3a-a413-38f251243baa",
      type: "polyline",
      sourceNodeId: "fcb96f10-720e-40e5-8ed0-ebdd0a46f234",
      targetNodeId: "ef34f09c-38ea-4ad4-acd2-cc2f464a2be6",
      startPoint: { x: 510, y: 245 },
      endPoint: { x: 850, y: 235 },
      text: { x: 680, y: 275, value: "否" },
      properties: {},
      pointsList: [
        { x: 510, y: 245 },
        { x: 510, y: 275 },
        { x: 850, y: 275 },
        { x: 850, y: 235 },
      ],
    },
  ],
};

const approveNodes = [
  {
    type: "apply",
    label: "提交申请",
    style: {
      width: "30px",
      height: "30px",
      borderRadius: "15px",
      border: "2px solid #FF6347",
    },
    property: {
      username: "",
      time: "",
      startTime: "",
      endTime: "",
    },
  },
  {
    type: "approver",
    label: "审批",
    style: {
      width: "30px",
      height: "30px",
      borderRadius: "4px",
      border: "2px solid #3CB371",
    },
  },
  {
    type: "jugement",
    label: "判断",
    style: {
      width: "30px",
      height: "30px",
      border: "2px solid #6495ED",
      transform: "rotate(45deg)",
    },
  },
  {
    type: "finsh",
    label: "结束",
    style: {
      width: "30px",
      height: "30px",
      borderRadius: "15px",
      border: "2px solid #FF6347",
    },
  },
];

function RegisteNode(lf) {
  class ApplyNodeModel extends CircleNodeModel {
    getConnectedTargetRules() {
      const rules = super.getConnectedTargetRules();
      const geteWayOnlyAsTarget = {
        message: "开始节点只能连出，不能连入！",
        validate: (source, target) => {
          let isValid = true;
          if (target) {
            isValid = false;
          }
          return isValid;
        },
      };
      // @ts-ignore
      rules.push(geteWayOnlyAsTarget);
      return rules;
    }
  }
  lf.register({
    type: "apply",
    view: CircleNode,
    model: ApplyNodeModel,
  });

  class ApproverNode extends RectNode {
    static extendKey = "UserTaskNode";
    getLabelShape() {
      const { x, y, width, height, properties } = this.props.model;
      const { labelColor, approveTypeLabel } = properties;
      return h(
        "text",
        {
          fill: labelColor,
          fontSize: 12,
          x: x - width / 2 + 5,
          y: y - height / 2 + 15,
          width: 50,
          height: 25,
        },
        approveTypeLabel
      );
    }
    getShape() {
      const { x, y, width, height, radius } = this.props.model;
      const style = this.props.model.getNodeStyle();
      return h("g", {}, [
        h("rect", {
          ...style,
          x: x - width / 2,
          y: y - height / 2,
          rx: radius,
          ry: radius,
          width,
          height,
        }),
        this.getLabelShape(),
      ]);
    }
  }
  class ApproverModel extends RectNodeModel {
    constructor(data, graphModel) {
      super(data, graphModel);
      this.properties = {
        labelColor: "#000000",
        approveTypeLabel: "",
        approveType: "",
      };
    }
  }

  lf.register({
    type: "approver",
    view: ApproverNode,
    model: ApproverModel,
  });

  class JugementModel extends PolygonNodeModel {
    constructor(data, graphModel) {
      super(data, graphModel);
      this.points = [
        [35, 0],
        [70, 35],
        [35, 70],
        [0, 35],
      ];
      this.properties = {
        api: "",
      };
    }
  }
  lf.register({
    type: "jugement",
    view: PolygonNode,
    model: JugementModel,
  });

  class FinshNodeModel extends CircleNodeModel {
    getConnectedSourceRules() {
      const rules = super.getConnectedSourceRules();
      const geteWayOnlyAsTarget = {
        message: "结束节点只能连入，不能连出！",
        validate: (source) => {
          let isValid = true;
          if (source) {
            isValid = false;
          }
          return isValid;
        },
      };
      // @ts-ignore
      rules.push(geteWayOnlyAsTarget);
      return rules;
    }
  }
  lf.register({
    type: "finsh",
    view: CircleNode,
    model: FinshNodeModel,
  });
}

export default {
  data() {
    return {
      /**
       * 节点和连线信息
       */
      data,

      /**
       * 所有节点类型
       */
      approveNodes,

      /**
       * 选中的节点数据
       */
      nodeData: null,
    };
  },
  lf: null,
  mounted() {
    const lf = new LogicFlow({
      container: this.$refs["container"],
      stopScrollGraph: true,
      stopZoomGraph: true,
      // width: 500,
      // height: 500,
      grid: {
        type: "dot",
        size: 20,
      },
      ...config,
    });

    RegisteNode(lf);

    lf.on("element:click", ({ data }) => {
      this.nodeData = data;
      console.log(JSON.stringify(lf.getGraphData()));
    });
    lf.on("connection:not-allowed", (data) => {
      this.$message.toast.error(data.msg);
    });

    lf.render(data);

    this.lf = lf;

    // const VueComponent = Vue.extend({
    //   props: {
    //     name: String,
    //   },
    //   render() {
    //     return <h1>{this.name || "none"}</h1>;
    //   },
    // });
    // var instance = new VueComponent();
    // instance.$mount(this.$refs.vv);

    // window.instance = instance;
  },
  methods: {
    dragNode(item) {
      this.lf.dnd.startDrag({
        type: item.type,
        text: item.label,
      });
    },
  },
};
</script>

<style lang="stylus">
.flow-container {
  width: 100%;
  height: 100%;

  .approve-example-container {
    position: relative;
    height: 100%;
  }

  .node-panel {
    position: absolute;
    top: 10px;
    left: 10px;
    width: 70px;
    padding: 20px 10px;
    background-color: white;
    box-shadow: 0 0 10px 1px rgb(228, 224, 219);
    border-radius: 6px;
    text-align: center;
    z-index: 101;
    user-select: none;

    .approve-node {
      display: inline-block;
      margin-bottom: 20px;
      display: flex;
      flex-direction: column;
      align-items: center;
    }

    .approve-node:hover {
      color: blue;

      .node-shape {
        border-color: blue;
      }
    }
  }

  .viewport {
    height: 100%;
  }

  .node-label {
    font-size: 12px;
    margin-top: 5px;
  }

  .node-jugement .node-label {
    margin-top: 15px;
  }

  .property-panel {
    position: absolute;
    bottom: 0;
    left: 0;
    width: 100%;
    padding: 20px;
    margin: 0;
    background-color: white;
    border-top-left-radius: 6px;
    border-top-right-radius: 6px;
    z-index: 101;
    box-shadow: 0 0 10px 1px rgb(228, 224, 219);
  }

  .property-panel-footer {
    width: 100%;
    text-align: center;
  }

  .property-panel-footer .property-panel-footer-hide {
    width: 200px;
  }

  .approve-example-container .lf-control {
    position: fixed;
    top: 10px;
    left: calc(50vw - 150px);
    width: 275px;
    box-shadow: 0 0 10px #888888;
  }

  .form-property {
    width: 30%;
  }

  .hover-panel {
    position: fixed;
  }
}
</style>