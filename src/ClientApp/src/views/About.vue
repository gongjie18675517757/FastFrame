<template>
  <VMap id="map_1" :center="center" :marks="marks">
    <InfoWindow :point="point">
      <h1>xxx</h1>
    </InfoWindow>
  </VMap>
</template>

<script>
import VMap from "@/components/Maps/Map.js";
import InfoWindow from "@/components/Maps/BMap/InfoWindow.vue";
export default {
  components: {
    VMap,
    InfoWindow
  },
  data() {
    return {
      center: {
        lng: 116.404,
        lat: 39.915,
        zoom: 15
      },
      point: {
        lng: 116.405,
        lat: 39.915
      },
      marks: [{ lat: 39.915, lng: 116.404, title: "xx" }]
    };
  }
};
</script>

<style scoped lang="stylus">
#map_1 {
  height: calc(100vh - 80px);
  overflow: auto;
}
</style>
