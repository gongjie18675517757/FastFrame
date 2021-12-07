<template>
  <div class="container">
    <div class="resize-element">
      改变大小试试
    </div>
    <div class="resize-record">
      触发了{{firedNum}}次resize事件。
    </div>
  </div>
</template>
 
<script>
export default {
  showName: '监听DOM变化',
  data () {
    return {
      observer: null,
      firedNum: 0,
      recordOldValue: { // 记录下旧的宽高数据，避免重复触发回调函数
        width: '0',
        height: '0'
      }
    }
  },
  mounted () {
    let MutationObserver = window.MutationObserver || window.WebKitMutationObserver || window.MozMutationObserver
    let element = document.querySelector('.resize-element')
    this.observer = new MutationObserver((mutationList) => {
      for (let mutation of mutationList) {
        console.log(mutation)
      }
      let width = getComputedStyle(element).getPropertyValue('width')
      let height = getComputedStyle(element).getPropertyValue('height')
      if (width === this.recordOldValue.width && height === this.recordOldValue.height) return
      this.recordOldValue = {
        width,
        height
      }
      this.firedNum += 1
    })
    this.observer.observe(element, { attributes: true, attributeFilter: ['style'], attributeOldValue: true })
  },
  beforeDestroyed () {
    if (this.observer) {
      this.observer.disconnect()
      this.observer.takeRecords()
      this.observer = null
    }
  }
}
</script>
 
<style lang="stylus" scoped>
.container
  position relative
  .resize-element
    transform translate(-50%, -50%)
    position absolute
    top 50%
    left 50%
    height 100%
    width 100%
    overflow hidden
    resize both
    display block
    box-shadow 0 0 1px 1px #3361D8
    border-radius 2px
</style>