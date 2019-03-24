export default {
  props: {
    value: Boolean,
    disabled: Boolean,
    label: String,
    description: String,
    errorMessages: Array
  },
  render(h) {
    if (this.disabled) {
      return h('span', null, this.value ? '是' : '否')
    }
    return h('v-checkbox', {
      props: {
        ...this.$attrs,
        value: this.value,
        readonly: this.disabled,
        label: this.label,
        description: this.description,
        errorMessages: this.errorMessages,
      },
      on: {
        ...this.$listeners,
        input: (val) => this.$emit('input', val),
        change: val => this.$emit('change', val)
      }
    })
  }
}