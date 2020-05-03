import {
  getValue,
  setValue,
  upload
} from '../../utils'
export default {
  props: {
    model: Object,
    value: String,
    disabled: Boolean,
    label: String,
    Name: String,
    errorMessages: Array,
    description:String,
    isXs: Boolean
  },
  render(h) {
    let info = getValue(this.model, this.Name.replace('_Id', ''))
    if (this.disabled && !this.isXs) {
      if (this.value) {
        return h('a', {
          on: {
            click: () => {
              window.open(`/api/resource/get/${this.value}/${info.Name}`)
            }
          }
        }, info.Name)
      } else {
        return h('span', null, '无')
      }
    }
    return h('v-text-field', {
      props: {
        ...this.props,
        'show-size': false,
        readonly: true,
        'append-icon':!this.disabled?'clear':'',
        value: this.value ? info.Name : null,
        placeholder:this.description
      },
      on: {
        click: () => {
          if (this.disabled)
            return;
          upload().then(([file]) => {
            this.$emit('input', file.Id);
            this.$emit('change', file);
            setValue(this.model, this.Name.replace('_Id', ''), file);
          })
        }
      }
    })
  }
}