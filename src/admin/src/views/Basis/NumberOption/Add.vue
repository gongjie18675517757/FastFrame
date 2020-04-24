<script>
    let pageInfo = {area: "Basis",name: "NumberOption",direction: "编号设置"};
    import Page from "@/components/Page/FormPageCore.js";
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
