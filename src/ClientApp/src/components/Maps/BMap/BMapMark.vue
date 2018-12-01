<template></template>

<script>
export default {
  props: {
    map: Object,
    lat: Number,
    lng: Number,
    title: String
  },
  data() {
    return {
      marker: null
    };
  },
  created() {
    if (this.map) {
      this.initMark();
    }
  },
  beforeDestroy() {
    if (this.map && this.marker) {
      this.map.removeOverlay(this.marker);
      this.marker.removeEventListener("click", this.emitClick);
    }
  },
  watch: {
    map(map) {
      if (map) {
        this.initMark();
      }
    },
    lat(val) {
      this.marker.setPosition(new BMap.Point(this.lng, val));
    },
    lng(val) {
      this.marker.setPosition(new BMap.Point(val, this.lat));
    },
    title(val) {
      this.marker.setTitle(val);
    }
  },
  methods: {
    initMark() {
      this.marker = new BMap.Marker(new BMap.Point(this.lng, this.lat), {
        title: this.title
      });
      this.map.addOverlay(this.marker);
      window.m = this.marker;

      this.marker.addEventListener("click", this.emitClick);
    },
    emitClick(e){
      this.$emit('click',e)
    }
  }
};
</script>

<style>
</style>
