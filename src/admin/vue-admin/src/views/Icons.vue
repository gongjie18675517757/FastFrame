<template>
  <div>
    <v-data-iterator
      :items="items"
      item-key="icon"
      :items-per-page="72"
      :footer-props="footerProps"
    >
      <template v-slot:header>
        <v-toolbar flat dense>
          <v-toolbar-title>所有图标</v-toolbar-title>
          <v-text-field
            style="padding-left: 15px"
            v-model="search"
            clearable
            flat
            hide-details
            prepend-inner-icon="search"
          ></v-text-field>
          <v-menu left bottom>
            <template v-slot:activator="{ on }">
              <v-btn text v-on="on" color="info">选择图标类型</v-btn>
            </template>
            <v-list>
              <v-list-item
                v-for="v in categorys"
                :key="v.key"
                @click="category = v.key"
                >{{ v.text }}</v-list-item
              >
            </v-list>
          </v-menu>

          <v-menu left bottom :close-on-content-click="false">
            <template v-slot:activator="{ on }">
              <v-btn text v-on="on" color="info">选择颜色</v-btn>
            </template>
            <v-color-picker v-model="color"></v-color-picker>
          </v-menu>
        </v-toolbar>
      </template>

      <template v-slot:default="props">
        <v-row style="max-height: calc(100vh - 146px);overflow:auto;">
          <v-col
            v-for="item in props.items"
            :key="item.name"
            cols="12"
            sm="4"
            md="3"
            lg="1"
            class="icon-div"
            @click="copy(item.icon)"
          >
            <div style="text-align: center">
              <v-icon :color="color">{{ item.icon }}</v-icon>
              <br />
              <span :style="{ color: color }">{{ item.icon }}</span>
            </div>
          </v-col>
        </v-row>
      </template>
    </v-data-iterator>
  </div>
</template>
<script>
import { ligth, regular } from "../Icons/materialdesignicons";
import { fontawesome } from "../Icons/fontawesome";
import { material } from "../Icons/material";
export default {
  data() {
    return {
      search: "",
      category: null,
      categorys: [
        {
          key: null,
          text: "全部",
        },
        {
          key: "materialdesignicons",
          text: "materialdesignicons",
        },
        {
          key: "material",
          text: "material",
        },
        {
          key: "fontawesome",
          text: "fontawesome",
        },
      ],
      color: "#000",
      footerProps: {
        "items-per-page-options": new Array(5)
          .fill(null)
          .map((_, i) => (i + 1) * 72),
      },
      ligth: ligth.map((v) => ({
        icon: `mdi-${v}`,
        category: "materialdesignicons",
      })),
      regular: regular.map((v) => ({
        icon: `mdi-${v}`,
        category: "materialdesignicons",
      })),
      fontawesome: fontawesome.map((v) => ({
        icon: `${v}`,
        category: "fontawesome",
      })),
      material: material.map((v) => ({
        icon: `${v}`,
        category: "material",
      })),
    };
  },
  computed: {
    items() {
      return [
        ...this.ligth,
        ...this.regular,
        ...this.fontawesome,
        ...this.material,
      ].filter(
        (v) =>
          v.icon.includes(this.search) &&
          (this.category || v.category) == v.category
      );
    },
  },
  methods: {
    copy(v) {
      const inputValue = document.createElement("input"); // 创建DOM元素
      document.body.appendChild(inputValue); // 将创建的DOM插入到Body上
      inputValue.value = v; // 将数据赋值给创建的DOM元素的Value上
      inputValue.select(); // 通过表单元素的select()方法选中内容
      document.execCommand("copy"); // 执行浏览器复制命令
      this.$message.toast.success("复制成功") // 复制完成后的提示
      document.body.removeChild(inputValue); // 移除DOM元素
    },
  },
};
</script>


<style lang="stylus">
.icon-div:hover {
  background: #f0f0f0;
  cursor: copy;
  border-border-radius: 5px;
}
</style>