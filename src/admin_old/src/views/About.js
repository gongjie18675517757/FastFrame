export default {
  props: ['id'],
  render(h) { 
    let arr = Array(210).fill(null).map(function (a,b) {
      return b
    }) 
    return h('div',null,arr.map(r => h('a', null, r)))
  }
}