export default {
  props: {
    value: String,
    disabled: Boolean,
    label: String,
    description: String,
    errorMessages: Array,
    values: {
      type: Array,
      default: function () {
        return []
      }
    },
    itemText: {
      type: String,
      default: 'Value'
    },
    itemValue: {
      type: String,
      default: 'Key'
    },
  },
  render(h) {
    if (this.disabled) {
      return h('span', {}, (this.values.find(r => r[this.itemValue] == this.value) || {})[this.itemText] || '')
    }
    return h('v-select', {
      props: {
        ...this.$attrs,
        value: this.value,
        readonly: this.disabled,
        label: this.label,
        description: this.description,
        errorMessages: this.errorMessages,
        items: this.values,
        itemText: this.itemText,
        itemValue: this.itemValue
      },
      on: {
        ...this.$listeners,
        input: (val) => {
          this.$emit('input', val)
        },
        change: val => this.$emit('change', val)
      }
    })
  }
}