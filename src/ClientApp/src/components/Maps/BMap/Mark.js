import {
  Base
} from "../mapUtils";
export default {
  mixins: [Base],
  props: {
    lat: Number,
    lng: Number,
    title: String
  },
  render(h) {
    return h('div', {}, this.$slots.default)
  },
  beforeDestroy() {
    if (this.map && this.instance) {
      this.map.removeOverlay(this.instance);
      this.instance.removeEventListener("click", this.emitClick);
    }
  },
  watch: {
    lat(val) {
      this.instance.setPosition(new BMap.Point(this.lng, val));
    },
    lng(val) {
      this.instance.setPosition(new BMap.Point(val, this.lat));
    },
    title(val) {
      this.instance.setTitle(val);
    }
  },
  methods: {
    load() {
      this.instance = new BMap.Marker(new BMap.Point(this.lng, this.lat), {
        title: this.title
      });
      this.map.addOverlay(this.instance);
      this.instance.addEventListener("click", this.emitClick);
    },
    emitClick(e) {
      this.$emit("click", e);
    }
  }
};