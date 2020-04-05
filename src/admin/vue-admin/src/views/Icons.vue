<template>
  <v-data-iterator :items="items" item-key="icon" :items-per-page="50" :footer-props="footerProps">
    <template v-slot:header>
      <v-toolbar flat dense>
        <v-toolbar-title>所有图标</v-toolbar-title>
        <v-text-field
          style="padding-left:15px;"
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
            <v-list-item v-for="v in categorys" :key="v.key" @click="category=v.key">{{v.text}}</v-list-item>
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
      <v-row>
        <v-col v-for="item in props.items" :key="item.name" cols="12" sm="6" md="4" lg="2">
          <div style="text-align:center">
            <v-icon :color="color">{{item.icon}}</v-icon>
            <br />
            <span :style="{color:color}">{{item.icon}}</span>
          </div>
        </v-col>
      </v-row>
    </template>
  </v-data-iterator>
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
          text: "全部"
        },
        {
          key: "materialdesignicons",
          text: "materialdesignicons"
        },
        {
          key: "material",
          text: "material"
        },
        {
          key: "fontawesome",
          text: "fontawesome"
        }
      ],
      color: "#000",
      footerProps: {
        "items-per-page-options": [50, 100, 200, 300, 400, 500]
      },
      ligth: ligth.map(v => ({
        icon: `mdi-${v}`,
        category: "materialdesignicons"
      })),
      regular: regular.map(v => ({
        icon: `mdi-${v}`,
        category: "materialdesignicons"
      })),
      fontawesome: fontawesome.map(v => ({
        icon: `${v}`,
        category: "fontawesome"
      })),
      material: material.map(v => ({
        icon: `${v}`,
        category: "material"
      }))
    };
  },
  computed: {
    items() {
      return [
        ...this.ligth,
        ...this.regular,
        ...this.fontawesome,
        ...this.material
      ].filter(
        v =>
          v.icon.includes(this.search) &&
          (this.category || v.category) == v.category
      );
    }
  }
};
</script>