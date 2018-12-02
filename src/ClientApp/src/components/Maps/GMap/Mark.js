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
      this.instance.setMap(null)
      // this.instance.removeListener("click", this.emitClick);
    }
  },
  watch: {
    lat(val) {
      this.instance.setPosition(google.maps.LatLng(this.lng, val));
    },
    lng(val) {
      this.instance.setPosition(google.maps.LatLng(val, this.lat));     
    },
    title(val) {
      this.instance.setTitle(val);
    }
  },
  methods: {
    load() {
      this.instance = new google.maps.Marker({
        position: {
          lng: this.lng,
          lat: this.lat
        },
        map: this.map,
        title: this.title,
      });     
      this.instance.addListener("click", this.emitClick);
    },
    emitClick(e) {
      this.$emit("click", e);
    }
  }
};