import {
  ListPageMixin,
  data,
  makeChildProps,
  makeChildListeners
} from "@/components/Page/ListPageCore.js";
import Vue from 'vue'

let dialog = {
  props: ['content'],
  render(h) {
    let c = h('div', {
      domProps: {
        innerHTML: this.content
      }
    })

    return h('v-container', {
      props: {
        'grid-list-xl': true,
        fluid: true,
        app: true,
      }
    }, [h('v-layout', {
      props: {
        'align-center': true,
        'justify-center': true,

      }
    }, [h('v-flex', {
      props: {
        xs12: true,
      }
    }, [h('v-card', null, [h('v-card-text', null, [c])])])])])
  }
}


export default {
  mixins: [ListPageMixin],
  data() {
    return {
      ...data,
      area: "Basis",
      name: "Notify",
      direction: "通知中心",
    };
  },
  methods: {
    getRequestUrl() {
      return '/api/notify/AllList'
    },
  },
  render(h) {
    let props = makeChildProps.call(this),
      listeners = makeChildListeners.call(this);

    listeners = {
      ...listeners,
      toEdit: ({
        Content
      }) => {
        this.$message.dialog(Vue.extend(dialog), {
          content: Content
        })
      }
    }

    return h("v-list-page", {
      props: {
        ...props,
        baseToolItems: [],
        direction: this.direction
      },
      on: {
        ...listeners,
      }
    });
  }
};