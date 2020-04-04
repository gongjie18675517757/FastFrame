import Mark from "./Mark";
import {
  Base
} from "../mapUtils";
export default {
  mixins: [Base],
  components: {
    Mark
  },
  props: {
    lng: Number,
    lat: Number
  },
  render(h) {
    return h('div', null, [
      this.$slots.default,
      h(Mark, {
        props: {
          lng: this.lng,
          lat: this.lat
        },
        on: {
          click: this.handleClick
        }
      })
    ])
  },
  methods: {
    load() {
      this.instance = new window.BMap.InfoWindow(this.$el, {
        width: 250,
        height: 80,
        title: "信息窗口",
        enableMessage: true
      });
    },
    handleClick({
      target
    }) {
      let {
        lng,
        lat
      } = target.getPosition();
      var point = new window.BMap.Point(lng, lat);
      this.map.openInfoWindow(this.instance, point);
    }
  }
};