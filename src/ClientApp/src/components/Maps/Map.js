import BMap from "./BMap/BMap.vue";
export default {
  props: {
    mode: {
      type: String,
      default: function () {
        return 'BMap'
      }
    }
  },
  data() {
    return {
      map: {
        BMap
      }
    };
  },
  render(h) {
    return h(this.map[this.mode], {
      props: this.$attrs
    }, this.$slots.default);
  }
};