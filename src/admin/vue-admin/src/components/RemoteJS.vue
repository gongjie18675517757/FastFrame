<script>
export default {
  props: {
    tag: {
      type: String,
      default: "script",
    },
    src: {
      type: String,
      required: true,
    },
  },
  render(h) {
    return h(this.tag, {
      attrs: {
        src: this.src,
      },
      on: {
        load: () => {
          this.$emit("load");
        },
      },
    });
  },
};
</script>