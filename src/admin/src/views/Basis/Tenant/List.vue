<script>
    let pageInfo = { area: "Basis", name: "Tenant", direction: "组织信息" };
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
