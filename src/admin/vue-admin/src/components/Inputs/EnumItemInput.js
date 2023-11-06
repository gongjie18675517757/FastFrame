import store from '../../store'
import TreeInput from '../Trees/TreeInput.vue'
export default {
    name: 'enum-item-input',
    functional: true,
    render(h, { props, children, listeners }) {
        const { value, disabled, errorMessages, description,/*label, , isXs, ,*/ EnumItemInfo, multiple } = props;
        return <TreeInput
            disabled={disabled}
            multiple={multiple}
            errorMessages={errorMessages}
            description={description}
            value={store.getters.getItemValues(EnumItemInfo)[value]}
            requestUrl="/api/EnumItem/TreeList"
            init_super_key={EnumItemInfo.toString()}
            onInput={ev => {
                listeners.input && listeners.input(ev.IntKey)
                listeners.change && listeners.change(ev)
            }}
        >
            {children}
        </TreeInput>
    }
}