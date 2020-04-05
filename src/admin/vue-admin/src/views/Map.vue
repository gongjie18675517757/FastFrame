<template>
  <VMap id="map_1" :center="center" :mode="mode">
    <InfoWindow v-bind="point">
      <v-card>
        <v-list>
          <v-list-item>
            <v-list-item-avatar>
              <img src="https://cdn.vuetifyjs.com/images/john.jpg" alt="John">
            </v-list-item-avatar>

            <v-list-item-content>
              <v-list-item-title>John Leider</v-list-item-title>
              <v-list-item-subtitle>Founder of Vuetify.js</v-list-item-subtitle>
            </v-list-item-content>

            <v-list-item-action>
              <v-btn icon>
                <v-icon>favorite</v-icon>
              </v-btn>
            </v-list-item-action>
          </v-list-item>
        </v-list>

        <v-divider></v-divider>

        <v-list>
          <v-list-item>
            <v-list-item-action>
              <v-switch color="purple"></v-switch>
            </v-list-item-action>
            <v-list-item-title>Enable messages</v-list-item-title>
          </v-list-item>

          <v-list-item>
            <v-list-item-action>
              <v-switch color="purple"></v-switch>
            </v-list-item-action>
            <v-list-item-title>Enable hints</v-list-item-title>
          </v-list-item>
        </v-list>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn text>Cancel</v-btn>
          <v-btn color="primary" text>Save</v-btn>
        </v-card-actions>
      </v-card>
    </InfoWindow>
    <Control>
      <v-radio-group v-model="mode" :mandatory="false">
        <v-radio label="百度地图" value="bd"></v-radio>
        <v-radio label="谷歌地图" value="gg"></v-radio>
      </v-radio-group>
    </Control>
    <Mark v-bind="center"/>
  </VMap>
</template>

<script>
import { Map, Control, InfoWindow, Mark } from "@/components/Maps";

export default {
  components: {
    VMap: Map,
    InfoWindow,
    Control,
    Mark
  },
  data() {
    return {
      center: {
        lng: 116.404,
        lat: 39.915,
        zoom: 15
      },
      point: {
        lng: 116.405,
        lat: 39.915
      },
      marks: [{ lat: 39.915, lng: 116.404, title: "xx" }]
    };
  },
  computed: {
    mode: {
      get() {
        return this.$store.getters.mapMode;
      },
      set(val) {
        this.$store.commit({
          type: "setMapMode",
          mode: val
        });
        window.location.reload();
      }
    }
  },
  methods: {
    toggle() {
      this.model = this.mode == "gg" ? "bd" : "gg";
    }
  }
};
</script>

<style scoped lang="stylus">
#map_1 {
  height: calc(100vh - 80px);
  overflow: auto;
}
</style>
