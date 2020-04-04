import {
  getValue,
  setValue,
  upload
} from '@/utils'
export default {
  props: {
    model: Object,
    value: String,
    disabled: Boolean,
    label: String,
    Name: String,
    errorMessages: Array,
    isXs: Boolean
  },
  render(h) {
    let info = getValue(this.model, this.Name.replace('_Id', ''))

    if (this.disabled) {
      if (this.value && info)
        return h('a', {
          on: {
            click: () => {
              window.open(`/api/resource/get/${info.Id}`)
            }
          }
        }, info.Name)
      else {
        return h('span', null, '无')
      }
    }

    let component = null
    if (this.value && info) {
      component = h('v-chip', {
        props: {
          close: true,
          color: 'info',
          label: true,
          outlined: true,
          title: info.Name
        },
        on: {
          input: () => {
            this.$emit('input', null)
            this.$emit('change', null)
            setValue(this.model, this.Name.replace('_Id', ""), null)
          }
        }
      }, `${info.Name}`)
    } else {
      component = h('v-input', null, [
        h('a', {
          on: {
            click: () => {
              upload().then(([result]) => {
                this.$emit('input', result.Id)
                this.$emit('change', result)
                setValue(this.model, this.Name.replace('_Id', ""), result)
              })
            }
          }
        }, '点击上传')
      ])
    }


    return h('div', {
      style: {
        'display': 'flex'
      }
    }, [
      component,
      h('v-messages', {
        props: {
          color: 'error',
          value: this.errorMessages
        }
      })
    ])
  }
}