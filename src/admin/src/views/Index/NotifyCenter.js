let pageInfo = {
  area: "Basis",
  name: "Notify",
  direction: "通知中心"
};
import {
  ListPageMixin,
  pageInjects,
  pageProps,
  makePageData,
  pageComputed,
  pageMethods,
  makeChildProps,
  makeChildListeners
} from "@/components/Page/ListPageCore.js";


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
  inject: pageInjects,
  props: pageProps,
  data() {
    let data = makePageData.call(this)
    return {
      ...data,
      ...pageInfo
    };
  },
  computed: pageComputed,
  methods: {
    ...pageMethods,
    getRequestUrl() {
      return '/api/notify/AllList'
    },
    toEdit(model) {
      this.$message.dialog(dialog, {
        content: model.Content
      })
    }
  },
  render(h) {
    let props = makeChildProps.call(this),
      listeners = makeChildListeners.call(this);

    listeners = {
      ...listeners
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