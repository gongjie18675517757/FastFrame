import {
  Base
} from "../mapUtils";
export default {
  mixins: [Base],
  props: {},
  render(h) {
    return h('div', {}, this.$slots.default)
  },
  data() {
    return {
      instance: null
    };
  },
  beforeDestroy() {
    if (this.map && this.instance) {
      this.map.removeControl(this.instance);
    }
  },
  methods: {
    load() {
      function control() {
        this.defaultAnchor = BMAP_ANCHOR_TOP_LEFT;
        this.defaultOffset = new BMap.Size(0, 0);
      }
      control.prototype = new BMap.Control();
      control.prototype.initialize = map =>
        map.getContainer().appendChild(this.$el);
      this.instance = new control();
      this.map.addControl(this.instance);
    }
  }
};