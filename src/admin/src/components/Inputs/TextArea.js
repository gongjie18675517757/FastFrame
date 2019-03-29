export default {
  props: {
    value: String,
    disabled: Boolean,
    label: String,
    description: String,
    errorMessages: Array,
    isXs: Boolean
  },
  render(h) {
    if (this.disabled && !this.isXs) {
      return h('span', null, this.value)
    }
    return h('v-textarea', {
      props: {
        ...this.$attrs,
        value: this.value,
        readonly: this.disabled,
        label: this.label,
        description: this.description,
        errorMessages: this.errorMessages,
        'auto-grow': true,
      },
      on: {
        ...this.$listeners,
        input: (val) => this.$emit('input', val),
        change: val => this.$emit('change', val)
      }
    })
  }
}