<script>
    let pageInfo = { area: "{{AreaName}}", name: "{{ModuleName}}", direction: "{{Description}}" };
    import Page from "@/components/Page/ListPageCore.js";
    export default {
        ...Page,
        data() {
            return {
                ...Page.data.call(this),
                ...pageInfo
            };
        }
    };
</script>
