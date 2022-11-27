import { getDownLoadPath } from '../../config'
import {
  getValue,
  setValue,
  upload
} from '../../utils'
export default {
  name: 'file-input',
  props: {
    model: Object,
    value: String,
    disabled: Boolean,
    label: String,
    Name: String,
    errorMessages: Array,
    description: String,
    RelateKeyFieldName: String,
    isXs: Boolean
  },
  render(h) {
    let file_id = getValue(this.model, this.RelateKeyFieldName)
    
    return h('v-text-field', {
      props: {
        ...this.props,
        'show-size': false,
        readonly: true,
        'append-icon': this.value ? 'mdi-download' : '',
        value: this.value,
        placeholder: this.description,
        dense: true,
        singleLine: !this.isXs,
        clearable: !this.disabled && !!this.value
      },
      on: {
        click: () => {
          if (this.disabled)
            return;
          upload().then(([file]) => {
            this.$emit('input', file.Name);
            this.$emit('change', file);
            setValue(this.model, this.RelateKeyFieldName, file.Id);
          })
        },
        'click:append': () => {
          window.open(getDownLoadPath(file_id, this.value));
        },
        'click:clear': () => {
          this.$emit('input', null);
          this.$emit('change', null);
          setValue(this.model, this.RelateKeyFieldName, null);
        },
      }
    },
      [
        h('template', { slot: 'prepend' }, this.$slots.prepend)
      ])
  }
}