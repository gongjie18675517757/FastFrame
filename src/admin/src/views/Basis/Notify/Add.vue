<script>
    let pageInfo = {area: "Basis",name: "Notify",direction: "通知"};
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
