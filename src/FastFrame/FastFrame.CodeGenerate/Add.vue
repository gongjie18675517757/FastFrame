<script>
    import {
        formData,
        pageProps,
        pageListeners,
        FormPageMixin
    } from "@/components/Page/FormPageCore.js";

    export default {
        mixins: [FormPageMixin],
        data() {
            return {
                ...formData,
                area: "{{AreaName}}",
                name: "{{ModuleName}}",
                direction: "{{Description}}"
            };
        },
        render(h) {
            let props = pageProps.call(this);
            let listeners = pageListeners.call(this);
            return h("v-page", { props, on: listeners });
        }
    };
</script>
