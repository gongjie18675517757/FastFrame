let pageInfo = {
  area: "Basis",
  name: "Notify",
  direction: "通知中心"
};

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

import Page from "@/components/Page/ListPageCore.js";

export default {
  ...Page,
  data() {
    return {
      ...Page.data.call(this),
      ...pageInfo
    };
  },
  methods: {
    ...Page.methods,
    getRequestUrl() {
      return '/api/notify/AllList'
    },
    toEdit(model) {
      this.$message.dialog(dialog, {
        content: model.Content
      })
    }
  }
};