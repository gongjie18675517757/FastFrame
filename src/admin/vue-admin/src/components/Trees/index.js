import BaseTree from './BaseTree'
export function makeTree({ requestUrl, type_name }) {
    return {
        functional: true,
        render(h, context) {
            const { children, listeners, props, data: { attrs } } = context;
            return h(BaseTree, {
                on: listeners,
                props: {
                    ...props,
                    ...attrs,
                    requestUrl: requestUrl || type_name ? `/api/${type_name}/TreeList` : null
                }
            }, children)
        }
    }
}