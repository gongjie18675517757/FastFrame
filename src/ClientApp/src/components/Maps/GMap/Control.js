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
      this.map.controls[google.maps.ControlPosition.TOP_LEFT].remove(this.$el);
    }
  },
  methods: {
    load() {
      this.map.controls[google.maps.ControlPosition.TOP_LEFT].push(this.$el);
    }
  }
};