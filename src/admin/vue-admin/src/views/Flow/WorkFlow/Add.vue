<script>
    let pageInfo = {area: "Flow",name: "WorkFlow",direction: "工作流"};
    import Page from "@/components/Page/FormPageCore.js";
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
