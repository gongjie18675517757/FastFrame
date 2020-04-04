export default {
  props: {
    model: Object,
    value: [String, Array],
    disabled: Boolean,
    label: String,
    errorMessages: Array,
    isXs: Boolean,
    description: String,
    EnumItemInfo: Object,
    multiple: Boolean,
    values: {
      type: Array,
      default: () => []
    }
  },
  data() {
    return {
      keyword: null
    }
  },
  computed: {
    /**
     * 可选值列表
     */
    items() {
      return this.values
    },

    /**
     * 字典文本
     */
    text() {
      let txt = '无'
      let val = this.value
      if (val) {
        if (!this.multiple) {
          let item = this.items.find(v => v.Key == val)
          if (item) txt = item.Value
        } else {
          val = val || []
          return this.items.filter(v => val.includes(v.Key)).map(v => v.Value).join(',')
        }
      }
      return txt;
    },

    /**
     * 传递给组件的值
     */
    childProps() {
      return {
        props: {
          value: this.value,
          items: this.items,
          clearable: !this.disabled,
          label: this.label,
          readonly: this.disabled,
          errorMessages: this.errorMessages,
          placeholder: this.placeholder,
          'item-text': 'Value',
          'item-value': 'Key',
          multiple: this.multiple
        },
        on: {
          input: val => {
            this.$emit('input', val)
            if (!this.multiple) {
              let item = this.items.find(v => v.Key == val)
              this.$emit('change', item)
            } else {
              val = val || []
              let item = this.items.filter(v => val.includes(v.Key))
              this.$emit('change', item)
            }
            this.keyword = null
          },
          'update:searchInput': val => this.keyword = val
        }
      }
    }
  },

  render(h) {
    /**
     * 不在小屏下,且只读时,显示一个文本就好了
     */
    if (this.disabled && !this.isXs) {
      return h('span', null, this.text)
    }



    return h('v-autocomplete', {
      ...this.childProps,
      scopedSlots: {
        selection: this.multiple ? ({
          item,
          index
        }) => {
          let text = ''
          text = index == 3 ? '...' : index > 3 ? '' : item.Value
          if (this.value.length - 1 > index && text && text != '...') {
            text += ','
          }
          return h('span', null, text)
        } : null
      }
    })
  }
}