export default {
    name: 'enum-item-input',
    props: {
        model: Object,
        value: [Number, Array],
        disabled: Boolean,
        label: String,
        errorMessages: Array,
        isXs: Boolean,
        description: String,
        EnumItemInfo: Number,
        multiple: Boolean
    },
    data() {
        return {
            keyword: null
        }
    },
    computed: {
        /**
         * 键名
         */
        itemKeyName() {
            return this.EnumItemInfo
        },

        /**
         * 可选值列表
         */
        items() {
            return this.$store.getters.getItemValues(this.itemKeyName)
        },

        /**
         * 是否有添加权限
         */
        hasAddItemPermission() {
            return this.$store.getters.existsPermission('EnumItem', 'Add')
        },

        /**
         * 字典文本
         */
        text() {
            let txt = '无'
            let val = this.value
            if (val) {
                if (!this.multiple) {
                    txt = this.items[val]
                } else {
                    val = val || []
                    return val.map(v => this.items[v]).join(';')
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
                    placeholder: this.description,
                    'item-text': 'Value',
                    'item-value': 'IntKey',
                    multiple: this.multiple,
                    dense: !this.isXs,
                    singleLine: !this.isXs,
                },
                on: {
                    input: val => {
                        this.$emit('input', val)
                        if (!this.multiple) {
                            let item = this.items.find(v => v.IntKey == val)
                            this.$emit('change', item)
                        } else {
                            val = val || []
                            let item = this.items.filter(v => val.includes(v.IntKey))
                            this.$emit('change', item)
                        }
                        this.keyword = null
                    },
                    'update:searchInput': val => this.keyword = val
                }
            }
        }
    },
    created() {
        
    },
    methods: {
        addItem() {
            this.$message.confirm({
                title: '提示',
                content: `确认添加<strong>${this.keyword}</strong>吗?`
            })
                .then(() => {
                    return this.$http.post('/api/EnumItem/Post', {
                        Key: this.itemKeyName,
                        Value: this.keyword,
                        Super_Id: this.SuperId
                    })
                }).then(item => {
                    this.$store.commit('addEnumItem', item)
                    this.keyword = null
                    this.$emit('input', item.Id)
                    this.$emit('change', item)
                })
        }
    },
    render(h) {


        let noData = h('v-list-item', {
            slot: 'no-data'
        }, [
            h('v-list-item-title', null, [
                h('a', {
                    on: {
                        click: this.addItem
                    }
                }, '添加进数据字典')
            ])
        ])

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
        }, [
            this.hasAddItemPermission && this.keyword ? noData : null,
            h('template', { slot: 'prepend' }, this.$slots.prepend)
        ])
    }
}