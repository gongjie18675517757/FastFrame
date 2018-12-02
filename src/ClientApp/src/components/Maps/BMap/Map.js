import {
  loadScript
} from "../mapUtils.js";
export default {
  provide() {
    return {
      getMap: () => this.map
    };
  },
  props: {
    center: {
      type: Object,
      default: function () {
        return {
          lat: 0,
          lng: 0,
          zoom: 10
        };
      }
    },
    marks: Array
  },
  data() {
    return {
      ak: "D31adcce22b47069a58026e05348bacc",
      map: null
    };
  },
  computed: {
    url() {
      return `http://api.map.baidu.com/api?v=3.0&ak=${this.ak}`;
    }
  },
  mounted() {
    loadScript(this.url, this.loaded);
  },
  methods: {
    loaded() {
      var map = new BMap.Map(this.$el);
      this.map = map;

      var top_right_navigation = new BMap.NavigationControl({
        anchor: BMAP_ANCHOR_BOTTOM_RIGHT,
        type: BMAP_NAVIGATION_CONTROL_SMALL
      });

      map.addControl(top_right_navigation);
      map.enableScrollWheelZoom(true);
      this.$watch("center", this.setCenter, {
        deep: true
      });
      this.setCenter(this.center);
    },
    setCenter({
      lng,
      lat,
      zoom
    }) {
      this.map.centerAndZoom(new BMap.Point(lng, lat), zoom);
    }
  },
  render(h) {
    return h('div', {}, this.$slots.default)
  }
};