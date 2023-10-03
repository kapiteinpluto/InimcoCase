<template>
  <div class="col fieldGroup">
    <label class="fancy-tag">
      {{ this.inputName }}
    </label>
    <input class="inputField" v-model="value" type="text" />
  </div>
</template>

<script>
export default {
  props: ["modelValue", "inputName"],
  emits: ["update:modelValue"],
  computed: {
    value: {
      get() {
        return this.modelValue;
      },
      set(newValue) {
        this.$emit("update:modelValue", newValue);
      },
    },
  },
  methods: {
    doSearch() {
      this.$router.push({
        name: "search",
        params: { searchString: this.searchString },
      });
    },
  },
};
</script>

<style lang="scss">
.inputField {
  /* color: #fff;
  background: linear-gradient(315deg, #42d3b3 25%, #647eff);
  border: none;
  padding: 5px 10px;
  margin: 5px;
  border-radius: 8px;
  cursor: pointer; */
  border: solid 0.1rem #969696;
  border-radius: 0.2rem;
  height: 1.8rem;
}
.fancy-tag {
  font-weight: bold;
  font-size: medium;
  text-transform: capitalize;
}
.fieldGroup {
  align-items: start;
}
</style>
