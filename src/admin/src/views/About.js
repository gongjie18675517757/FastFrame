export default {
  props: ['id'],
  render(h) {

    return h('span', null, JSON.stringify(this.$route))
  }
}