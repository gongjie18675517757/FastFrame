import Table from "./DataTable.vue";
import {
  getDefaultModel,
  getModelObjectItems,
  getRules,
  getColumns
} from "@/generate";
import {
  distinct
} from '@/utils'
import { upload, getIconFunc } from "../../utils";
import { getDownLoadPath } from "../../config";

let defArray = {
  type: Array,
  default: () => ([])
}
let defFunc = {
  type: Function,
  default: v => v
}

/**
 *  表格
 */
export default Table;

/**
 * 基础详情列表
 */
export const BasisDetaiTable = {
  props: {
    value: defArray,
    title: String,
    columns: defArray,
    toolItems: defArray,
    rowKey: {
      type: String,
      default: 'Id'
    },
    loading: Boolean
  },
  data() {
    return {
      selection: []
    }
  },
  render(h) {
    return h('v-card', {
      props: {
        tile: true
      }
    }, [
      h('v-toolbar', {
        props: {
          dense: true,
          flat: true,
          color: 'transparent',
          height: '30px'
        }
      }, [
        h('v-toolbar-title', null, this.title),
        ...this.toolItems.map(v => v.render ? v.render(h) : h('v-btn', {
          key: v.name,
          props: {
            ...v,
            icon: !!v.icon
          },
          on: {
            click: () => this.$emit(`itemClick:${v.name}`, this.selection)
          }
        }, [v.icon ? h('v-icon', null, v.icon) : null, h('span', null, v.caption)]))
      ]),
      // h('v-divider'),
      h('v-card-text', {
        class: ['form-content']
      }, [
        h(Table, {
          props: {
            items: this.value,
            columns: this.columns,
            rowKey: this.rowKey,
            loading: this.loading,
            ...this.$attrs
          },
          on: {
            ...this.$listeners,
            input: val => this.selection = val
          }
        })
      ])
    ])
  }
}

/**
 * 
 */
export const FormDetailObj = {
  props: {
    value: defArray,
    model: Object,
    title: String,
    canEdit: Boolean,
    typeName: String,
    frmColsfunc: defFunc,
    frmFormFunc: defFunc,
    frmOptionFunc: defFunc,
    frmRules: defFunc
  },
  data() {
    return {
      columns: []
    }
  },
  computed: {
    dynamicColumns() {
      if (this.canEdit) {
        return [{
          Name: 'Operate',
          Description: '操作',
          width: '150px',
          component: {
            props: ['model'],
            render(h) {
              return h('span', null, [
                h('v-btn', {
                  props: {
                    text: true,
                    icon: true,
                    small: true
                  },
                  on: {
                    click: () => this.$emit('edit', this.model)
                  }
                }, [h('v-icon', null, 'edit')]),
                h('v-btn', {
                  props: {
                    text: true,
                    icon: true,
                    small: true
                  },
                  on: {
                    click: () => this.$emit('remove', this.model)
                  }
                }, [h('v-icon', null, 'delete')]),
              ])
            }
          }
        }].concat(this.columns)
      } else {
        return this.columns;
      }
    },
    dynamicToolItems() {
      let arr = [{
        text: true,
        small: true,
        color: 'primary',
        caption: '添加',
        name: 'add'
      }]
      if (this.canEdit)
        return arr;
      else
        return []
    }
  },
  created() {
    getColumns(this.typeName)
      .then(this.frmColsfunc)
      .then(cols => distinct(cols, v => v.Name, (a, b) => ({
        ...a,
        ...b
      })))
      .then(cols => this.columns = cols)
  },
  methods: {
    insert(model) {
      let
        options = null,
        rules = null
      return Promise.resolve(model)
        .then(this.frmFormFunc)
        .then(frm => model = frm)
        .then(() => getModelObjectItems(this.typeName))
        .then(opts => distinct(opts, v => v.Name, (a, b) => ({
          ...a,
          ...b
        })))
        .then(this.frmOptionFunc)
        .then(opts => options = opts)
        .then(() => getRules(this.typeName))
        .then(rus => rules = rus)
        .then(() => this.$message.prompt({
          model,
          options,
          rules,
          title: `添加${this.title}`,
          width: '500px'
        }))
        .then(this.frmFormFunc)

    },
    add() {
      getDefaultModel(this.typeName).then(this.insert).then(model => {
        this.value.push(model)
        this.$emit('change', this.value)
      })
    },
    edit(item) {
      let index = this.value.findIndex(v => v == item);
      let model = JSON.parse(JSON.stringify(item))
      return Promise.resolve(model).then(this.insert).then(model => {
        this.value.splice(index, 1, model)
        this.$emit('change', this.value)
      })
    },
    remove(item) {
      let index = this.value.findIndex(v => v == item);
      this.value.splice(index, 1)
      this.$emit('change', this.value)
    }
  },
  render(h) {
    return h(BasisDetaiTable, {
      props: {
        value: this.value,
        title: this.title,
        columns: this.dynamicColumns,
        toolItems: this.dynamicToolItems
      },
      on: {
        ...this.$listeners,
        edit: this.edit,
        remove: this.remove,
        'itemClick:add': this.add
      }
    })
  }
}

/**
 * 表单型详情列表
 */
export const FormDetailTable = function () {
  return {
    ...FormDetailObj
  }
}

/**
 * 
 */
export const SelectDetailObj = {
  ...FormDetailTable,
  props: {
    ...FormDetailTable.props,
    dialogWidth: String,
    dialogComponent: {
      type: [Object, Function],
      required: true
    }
  },
  computed: {
    ...FormDetailTable.computed,
    dynamicColumns() {
      if (this.canEdit) {
        return [{
          Name: 'Operate',
          Description: '操作',
          width: '150px',
          component: {
            props: ['model'],
            render(h) {
              return h('span', null, [
                h('v-btn', {
                  props: {
                    text: true,
                    icon: true,
                    small: true
                  },
                  on: {
                    click: () => this.$emit('remove', this.model)
                  }
                }, [h('v-icon', null, 'delete')]),
              ])
            }
          }
        }].concat(this.columns)
      } else {
        return this.columns;
      }
    }
  },
  methods: {
    ...FormDetailTable.methods,
    add() {
      this.$message.dialog(this.dialogComponent, {
        width: this.dialogWidth
      }).then(rows => {
        return Promise.all(rows.map(v => this.frmFormFunc(v)))
      }).then(rows => {
        this.value.push(...rows)
        this.$emit('change', this.value)
      })
    }
  }
}

/**
 * 选择型详情列表
 */
export const SelectDetailTable = function () {
  return {
    ...SelectDetailObj
  }
}


/**
 * 文件列表
 */
export const FileDetailObj = {
  props: {
    value: defArray,
    model: Object,
    title: String,
    canEdit: Boolean,
    fileKey: String,
    accept: {
      type: String,
      default: '*/*'
    },
    verifyFileFunc: Function
  },
  data() {
    return {
      columns: [],
      loading: false,
    }
  },
  created() {
    getColumns('ResourceModel').then(arr => this.columns = distinct([

      ...arr,
      {
        Name: 'Name',
        renderFunc: (h, { value, model }) => {
          let src = getIconFunc(model);
          return h('div', null, [
            h('v-avatar', {
              props: {
                size: '24px'
              }
            }, [
              h('img', {
                attrs: {
                  src
                }
              })
            ]),
            h('a', {
              style: {
                'padding-left': '5px'
              },
              on: {
                click: () => {
                  window.open(getDownLoadPath(model.Id, model.Name))
                }
              }
            }, value)
          ])
        }
      },
      {
        Name: 'Size',
        getValueFunc: ({ value }) => value > 1024 * 1024 ? `${(value / 1024 / 1024).toFixed(2)}mb` : `${(value / 1024).toFixed(2)}kb`
      }
    ], v => v.Name, (a, b) => ({ ...a, ...b })))
  },
  computed: {
    rows() {
      return this.value.map(v => v.Key == this.fileKey)
    }
  },
  methods: {
    onProgress(v) {
      this.loading = true;

      if (v >= 100) {
        this.loading = false;
      }
    },
    verifyFile(arr) {
      if (typeof this.verifyFileFunc == 'function') {
        return this.verifyFileFunc(arr)
      }

      return true;
    }
  },
  render(h) {
    return h(BasisDetaiTable, {
      props: {
        title: this.title,
        value: this.value,
        loading: this.loading,
        columns: [
          ...(this.canEdit ? [
            {
              Name: 'Operate',
              Description: '操作',
              renderFunc: (h, { model }) => {
                return h('a', {
                  on: {
                    click: () => {
                      this.$message.confirm({
                        title: '提示',
                        content: '确认要移除这一项吗?'
                      }).then(() => {

                        let index = this.value.findIndex(v => v == model);
                        this.value.splice(index, 1)
                      })
                    }
                  }
                }, '移除')
              }
            }
          ] : []),
          ...this.columns
        ],
        toolItems: [
          {
            render: (h) => {
              return this.canEdit ? h('v-btn', {
                props: {
                  small: true,
                  text: true,
                  color: 'primary'
                },
                on: {
                  click: () => {
                    upload({
                      accept: this.accept,
                      onProgress: this.onProgress,
                      verifyFileFunc: this.verifyFile
                    }).then(([file]) => {
                      this.value.push({
                        Id: file.Id,
                        ContentType: file.ContentType,
                        Key: this.fileKey,
                        Name: file.Name,
                        Size: file.Size,
                        UploaderName: this.$store.state.currUser.Name,
                        UploadTime: file.UploadTime
                      });
                      this.$emit('input', this.value)
                      this.$emit('change', this.value)
                    })
                  }
                }
              }, '上传') : null
            }
          }
        ]
      }
    })
  }
}

/**
 * 文件列表
 */
export const FileDetailTable = function () {
  return {
    ...FileDetailObj
  }
}