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
    itemText: String,
    itemValue: String,
  },
  render(h) {
    return h('v-select', {
      props: {
        ...this.$attrs,
        value: this.value,
        readonly: this.disabled,
        label: this.label,
        description: this.description,
        errorMessages: this.errorMessages,
        items: this.values,
        itemText: this.itemText || 'Value',
        itemValue: this.itemValue || 'Key'
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