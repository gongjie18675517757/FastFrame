export default {
  props: {
    value: String,
    disabled: Boolean,
    label: String,
    description: String,
    errorMessages: Array
  },
  render(h) {
    return h('v-text-field', {
      props: {
        ...this.$attrs,
        value: this.value,
        readonly: this.disabled,
        label: this.label,
        description: this.description,
        errorMessages: this.errorMessages,
        'auto-grow': true,
        'hide-details': true,
        title:'只读'
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