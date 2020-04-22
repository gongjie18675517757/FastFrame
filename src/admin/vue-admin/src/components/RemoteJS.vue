<script>
export default {
  props: {
    src: {
      type: String,
      required: true
    }
  },
  render(h) {
    return h("script", {
      attrs: {
        type: "text/javascript",
        src: this.src // "/libs/js/go.js"
      },
      on: {
        load: () => {
          this.$emit("load");
        }
      }
    });
  }
};
</script>