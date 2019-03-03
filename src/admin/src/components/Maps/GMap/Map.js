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
      key: "AIzaSyBCuVCymbGkrc8euKouSkItwRdHzwlxZPk",
      map: null
    };
  },
  computed: {
    url() {
      return `http://maps.google.cn/maps/api/js?key=${this.key}&language=CH&region=CH&libraries=drawing`;
    }
  },
  mounted() {
    loadScript(this.url, this.loaded);
  },
  methods: {
    loaded() {
      var map = new window.google.maps.Map(this.$el); 
      this.map = map;  
      this.$watch("center", this.setCenter, {
        deep: true
      });
      this.setCenter(this.center);
    },
    load(){
      
    },
    setCenter({
      lng,
      lat,
      zoom
    }) {
      var position = new window.google.maps.LatLng(lat, lng);
      this.map.setCenter(position);
      this.map.setZoom(zoom);
    }
  },
  render(h) {
    return h('div', {}, this.$slots.default)
  }
};