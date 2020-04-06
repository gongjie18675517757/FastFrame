<script>
    let pageInfo = { area: "Basis", name: "Dept", direction: "部门" };
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
