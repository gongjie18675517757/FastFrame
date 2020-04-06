<script>
    let pageInfo = {area: "Basis",name: "User",direction: "用户"};
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
