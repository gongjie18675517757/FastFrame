<script>
    let pageInfo = { area: "{{AreaName}}", name: "{{ModuleName}}", direction: "{{Description}}" };
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
