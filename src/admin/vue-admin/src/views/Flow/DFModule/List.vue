<script>
    let pageInfo = { area: "Flow", name: "DFModule", direction: "动态表单模块" };
    import Page from "../../../components/Page/ListPageCore.js";
    export default {
        ...Page,
        data() {
            let data = Page.data.call(this);
            return {
                ...data,
                ...pageInfo
            };
        }
    };
</script>
