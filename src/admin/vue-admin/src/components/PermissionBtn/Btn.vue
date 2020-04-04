 <script>
export default {
  props: {
    name: String,
    moduleName: String,
    title: String,
    disabled: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {};
  },
  computed: {
    evalDisabled() {
      return this.disabled || !this.permission;
    },
    evalTitle() {
      if (!this.permission) return `${this.title}(权限不足)`;
      else return this.title;
    },
    permission() {
      return this.$store.getters.existsPermission(this.moduleName, this.name);
    }
  },
  methods: {
    handleClick($event) {
      this.$emit("click", $event);
    }
  },
  render(h) {
    return h(
      "v-btn",
      {
        props: {
          ...this.$attrs,
          disabled: this.evalDisabled,
          title: this.evalTitle
        },
        on: {
          click: () => this.handleClick()
        }
      },
      [this.$slots.default]
    );
  }
};
</script>
 
