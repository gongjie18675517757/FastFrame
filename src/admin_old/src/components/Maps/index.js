/**
 * 地图组件
 */
export const Map = {
  provide() {
    return {
      getMode: () => this.mode
    }
  },
  props: {
    mode: String
  },
  data() {
    return {
      dic: {
        bd: () => import('./BMap/Map'),
        gg: () => import('./GMap/Map')
      }
    };
  },
  render(h) {
    return h(this.dic[this.mode], {
      props: this.$attrs
    }, this.$slots.default);
  }
};

/**
 * 抽象组件基类,使之适应多厂商地图
 */
const Base = {
  inject: ['getMode'],
  data() {
    return {
      dic: {}
    };
  },
  computed: {
    mode() {
      return this.getMode()
    }
  },
  render(h) {
    return h(this.dic[this.mode], {
      props: this.$attrs
    }, this.$slots.default);
  }
}

/**
 * 地图控件
 */
export const Control = {
  mixins: [Base],
  data() {
    return {
      dic: {
        bd: () => import('./BMap/Control'),
        gg: () => import('./GMap/Control')
      }
    };
  }
};

/**
 * 标记
 */
export const Mark = {
  mixins: [Base],
  data() {
    return {
      dic: {
        bd: () => import('./BMap/Mark'),
        gg: () => import('./GMap/Mark')
      }
    };
  }
}

/**
 * 信息窗口
 */
export const InfoWindow = {
  mixins: [Base],
  data() {
    return {
      dic: {
        bd: () => import('./BMap/InfoWindow'),
        gg: () => import('./GMap/InfoWindow')
      }
    };
  }
}