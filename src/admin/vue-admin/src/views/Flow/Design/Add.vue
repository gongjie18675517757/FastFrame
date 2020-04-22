<template>
  <div>
    <RemoteJs :src="dependJs" @load="handleJsLoad" />
    <div style="width:100%; white-space:nowrap;">
      <!--  控件 -->
      <span style="display: inline-block; vertical-align: top; padding: 5px; width:110px">
        <div ref="myPaletteDiv" style="border: solid 1px black; height: 420px"></div>
      </span>

      <!--  设计面板 -->
      <span style="display: inline-block; vertical-align: top; padding: 5px; width:80%">
        <div ref="myFlowDesignerDiv" style="border: solid 1px black; height: 420px"></div>
      </span>
    </div>
  </div>
</template>

<script>
import RemoteJs from "../../../components/RemoteJS.vue";
import FlowDesigner from "../flow-desinger";
export default {
  components: {
    RemoteJs
  },
  data() {
    return {
      dependJs: "/libs/js/go.js"
    };
  },
  methods: {
    handleJsLoad() {
      // 流程图设计器
      var myDesigner = (window.myDesigner = new FlowDesigner({
        el: this.$refs.myFlowDesignerDiv,
        onDiagramModified: this.onDiagramModified,
        onSelectionDeleting: this.onSelectionDeleting,
        onEditNode:this.onEditNode
      }));

      // 初始化控件面板
      myDesigner.initToolbar(this.$refs.myPaletteDiv);

      // 在设计面板中显示流程图
      myDesigner.displayFlow({
        class: "go.GraphLinksModel",
        modelData: { position: "-5 -5" },
        nodeDataArray: [
          // {
          //   key: "1",
          //   text: "开始",
          //   figure: "Circle",
          //   fill: "#4fba4f",
          //   stepType: 1,
          //   loc: "90 110"
          // },
          // {
          //   key: "2",
          //   text: "结束",
          //   figure: "Circle",
          //   fill: "#CE0620",
          //   stepType: 4,
          //   loc: "770 110"
          // },
          // { key: "3", text: "填写请假信息 ", loc: "210 110", remark: "" },
          // { key: "4", text: "部门经理审核 ", loc: "370 110", remark: "" },
          // { key: "5", text: "人事审核  ", loc: "640 110", remark: "" },
          // { key: "6", text: "副总经理审核  ", loc: "510 40", remark: "" },
          // { key: "7", text: "总经理审核  ", loc: "500 180", remark: "" }
        ],
        linkDataArray: [
          // { from: "1", to: "3" },
          // { from: "3", to: "4" },
          // { from: "4", to: "5" },
          // { from: "5", to: "2" },
          // {
          //   from: "4",
          //   to: "6",
          //   key: "1001",
          //   text: "小于5天 ",
          //   remark: "",
          //   condition: "Days<5"
          // },
          // { from: "6", to: "5" },
          // {
          //   from: "4",
          //   to: "7",
          //   key: "1002",
          //   text: "大于5天 ",
          //   remark: "",
          //   condition: "Days>5"
          // },
          // { from: "7", to: "5" }
        ]
      });
    },

    /**
     * 图形有变化时
     */
    onDiagramModified() {
     
    },
    /**
     * 节点删除时
     */
    onSelectionDeleting(e) {
      e.cancel = false;
    },
    /**
     * 更新节点信息
     */
    onEditNode(node,updateTextFunc){
      let txt= prompt('输入节点名称','')
      updateTextFunc(txt);
    }
  }
};
</script>