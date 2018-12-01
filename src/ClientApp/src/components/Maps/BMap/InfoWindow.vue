<template>
  <div>
    <slot></slot>
    <Mark v-bind="point" :map="map" @click="handleClick"/>
  </div>
</template>

<script>
import Mark from "./BMapMark.vue";
export default {
  components: {
    Mark
  },
  props: {
    point: Object
  },
  data() {
    return {
      infoWindow: null
    };
  },
  computed: {
    map() {
      return this.$parent.map;
    }
  },
  created() {
    if (this.map) this.setInfoWindow();
  },
  watch: {
    map(val) {
      if (val) this.setInfoWindow();
    }
  },
  methods: {
    setInfoWindow() {
      this.infoWindow = new BMap.InfoWindow(this.$el, {
        width: 250, // 信息窗口宽度
        height: 80, // 信息窗口高度
        title: "信息窗口", // 信息窗口标题
        enableMessage: true //设置允许信息窗发送短息
      });
    },
    handleClick(e) {
      var p = e.target;
      var point = new BMap.Point(p.getPosition().lng, p.getPosition().lat);
      this.map.openInfoWindow(this.infoWindow, point);
    }
  }
};
</script>

<style>
</style>
