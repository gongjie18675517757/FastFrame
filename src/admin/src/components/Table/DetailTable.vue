<script>
import { getValue } from "@/utils";
import Table from "./DataTable.vue";
export default {
  props: {
    title: String,
    model: Object,
    canEdit: Boolean,
    propName: String,
    columns: Array,
    handleAdd: Function,
    handleUpdate: Function,
    HaveDelete: {
      type: Boolean,
      default: true
    },
    toolButtons: {
      type: Array,
      default: function() {
        return [];
      }
    }
  },
  computed: {
    items() {
      return getValue(this.model, this.propName);
    }
  },
  methods: {
    async add() {
      await this.handleAdd(this.items);
      this.$emit("change", this.items);
      this.$emit("input", this.items);
    },
    remove(item) {
      let index = this.items.findIndex(r => r == item);
      if (index >= 0) this.items.splice(index, 1);
      this.$emit("change", this.items);
      this.$emit("input", this.items);
    },
    update(item) {
      this.handleUpdate(item).then(r => {
        let index = this.items.findIndex(x => x == item);
        if (index > -1) {
          this.items.splice(index, 1, r);
        }
      });
    }
  },
  render(h) {
    let self = this;
    let toolButtons = this.toolButtons;
    let operateCell = {
      props: ["model"],
      render(h) {
        return h("span", null, [
          self.HaveDelete
            ? h(
                "a",
                {
                  on: {
                    click: () => {
                      this.$emit("remove-item", this.model);
                    }
                  }
                },
                "删除"
              )
            : null,
          self.handleUpdate
            ? h(
                "a",
                {
                  on: {
                    click: () => {
                      this.$emit("update-item", this.model);
                    }
                  }
                },
                "修改"
              )
            : null,
          ...toolButtons.map(v => v(h, this.model))
        ]);
      }
    };
    let tb = h(Table, {
      props: {
        rows: this.items,
        columns: [
          ...(this.canEdit
            ? [
                {
                  name: "",
                  Description: "操作",
                  template: operateCell
                }
              ]
            : []),
          ...this.columns
        ]
      },
      on: {
        "remove-item": $event => {
          this.remove($event);
        },
        "update-item": $event => {
          this.update($event);
        },
        ...this.$listeners
      }
    });

    return h("v-flex", { attrs: { xs12: true } }, [
      h("v-card", {}, [
        h(
          "v-toolbar",
          {
            props: {
              flat: true,
              dense: true,
              card: true,
              color: "transparent"
            }
          },
          [
            h("v-toolbar-title", null, this.title),
            h("v-spacer"),
            this.canEdit && this.handleAdd
              ? h(
                  "v-btn",
                  {
                    props: {
                      icon: true
                    },
                    on: {
                      click: () => this.add()
                    }
                  },
                  [h("v-icon", null, "add")]
                )
              : null
          ]
        ),
        h("v-card-text", null, [tb])
      ])
    ]);
  }
};
</script>

<style>
</style>
