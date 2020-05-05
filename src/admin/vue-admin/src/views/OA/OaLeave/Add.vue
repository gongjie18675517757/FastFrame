<script>
    let pageInfo = {area: "OA",name: "OaLeave",direction: "请假单"};
    import Page from "../../../components/Page/FormPageCore.js";
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
