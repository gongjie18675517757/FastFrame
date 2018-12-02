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
        },
        ref: 'mark',
      })
    ])
  },
  methods: {
    load() {
      this.instance = new google.maps.InfoWindow({
        content: this.$el,
      });

    },
    handleClick({
      target
    }) {
      this.instance.open(this.map,this.$refs.mark.instance)
    }
  }
};